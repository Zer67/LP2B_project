using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ball_spawner : MonoBehaviour
{
    /* References */
    private ball_behavior ball_prefab;

    /* Variables */
    private List<ball_behavior> balls = new List<ball_behavior>(3);
    private int ball_number = 1;

    
    // Start is called before the first frame update
    void Start()
    {
        ball_prefab = Resources.Load<ball_behavior>("Prefabs/Ball");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void decreaseBallNumber()
    {
        --this.ball_number;
    }

    /**********************************************************************************/

    public int getBallNumber(){return this.ball_number;}


    /**
      *The following methods are for managing the multiple balls spawning.
      *Unfortunately they have been causing some issues, so 
      *we decided to not use them.
      **/

    public void spawnBalls(Vector3 position,int count)
    {
        for(int i = 0;i<count;++i)
        {
            ball_behavior spawnedBall = Instantiate(ball_prefab,position, Quaternion.identity) as ball_behavior;
            spawnedBall.gameObject.name = "Ball";
            Rigidbody2D ballBody = spawnedBall.GetComponent<Rigidbody2D>();
            ballBody.isKinematic = false;
            ballBody.freezeRotation = true;
            ballBody.velocity = (new Vector2(0,ball_prefab.getBallSpeed())).normalized * ball_prefab.getBallSpeed();
            balls.Add(spawnedBall); //Keeping track of the current spawed balls in a list
            ball_number++;
        }
    }

    /**********************************************************************************/

    public void removeBall(ball_behavior b) 
    {
        int index = balls.IndexOf(b);
        balls.Remove(b);
        Destroy(b.gameObject);

        if(index == 0)
        {
            foreach(ball_behavior tmp in balls)
            {
                int i = balls.IndexOf(tmp);
                balls[0] = tmp;
                balls.RemoveAt(i);
                break;
            }
        }
        
    }

    /**********************************************************************************/

    public void reset()
    {
        if(ball_number > 1)
        {
            foreach(ball_behavior tmp in balls)
            {
                if(balls.IndexOf(tmp) != 0) 
                {
                    balls.Remove(tmp);
                    Destroy(tmp.gameObject);
                }
            }
            
        }
    }

}
