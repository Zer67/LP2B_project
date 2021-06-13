using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ball_spawner : MonoBehaviour
{
    private ball_behavior ball_prefab;
    private ArrayList balls = new ArrayList();
    private int ball_number = 0;
    // Start is called before the first frame update
    void Start()
    {
        ball_prefab = Resources.Load<ball_behavior>("Prefabs/Ball");
        spawnBalls(new Vector3(0,-5.5f,0),1);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

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
            balls.Add(spawnedBall);
            ball_number++;
        }
    }

    public void decreaseBallNumber()
    {
        --this.ball_number;
    }

    public int getBallNumber(){return this.ball_number;}

    public void reset()
    {
        if(balls.Count > 1)
        {
            for(int i = 1;i<ball_number;++i)
            {
                ball_behavior ball = (ball_behavior)balls[i];
                Destroy(ball.gameObject);
            }
        }
    }

    public void removeBall(ball_behavior ball)
    {
        int index = balls.IndexOf(ball);
        for(int i = 0;i<index;++i)
        {
            if(balls[i] == null)
            {
                int tmp = i;
                for(int j = i+1;j<=index;++j)
                {
                    balls[tmp] = balls[j];
                    ++tmp;
                }
            }
        }
    }

     
}
