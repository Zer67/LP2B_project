using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class powerups_behavior : MonoBehaviour
{
    private ball_behavior ball;

    private paddle_behavior paddle;

    private ball_spawner spawner_script;

    private powerups_spawner powerup;

    // Start is called before the first frame update
    void Start()
    {
        powerup = FindObjectOfType<powerups_spawner>();
        spawner_script = FindObjectOfType<ball_spawner>();
        paddle = FindObjectOfType<paddle_behavior>();
        GetComponent<Rigidbody2D>().velocity = new Vector2(0,-5f);
        ball = FindObjectOfType<ball_behavior>();
    }

    // Update is called once per frame
    void Update()
    {
        if(transform.position.y < -10){
            Destroy(gameObject);
        }

        
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if(other.gameObject.name == "Paddle"){
            paddle_behavior paddle = other.gameObject.GetComponent<paddle_behavior>();
            if(gameObject.name == "doge_coin") paddle.updateScore(100);
            else
            {
                ball = FindObjectOfType<ball_behavior>();
                ball.resetAll();
                switch(gameObject.name)
                {
                    case "Catch":
                        powerup.setSticky(true);
                    break;
                    case "Disruption":
                        spawner_script.spawnBalls(ball.transform.position,2);
                    break;
                    case "Enlarge":
                        other.gameObject.transform.localScale += new Vector3(0.2f,-0.2f,0);
                    break;
                    case "Laser":
                        other.gameObject.GetComponent<paddle_behavior>().canFireBullets(true);
                    break;
                    case "Slow":
                        paddle.setBallSpeed(7.0f);
                    break;
                }
            }
            Destroy(gameObject);
        }
    }
}
