using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class brick_behavior : MonoBehaviour
{
    private int lives = 1;
    
    private bool indestructible;

    private ball_spawner balls;

    private powerups_spawner powerup;

    private brickCreation_script brick_spawner;
    // Start is called before the first frame update
    void Start()
    {
        powerup = FindObjectOfType<powerups_spawner>();
        brick_spawner = FindObjectOfType<brickCreation_script>();
        balls = FindObjectOfType<ball_spawner>();
    }

    // Update is called once per frame
    void Update()
    {
        gameObject.name = "Brick";
    }

    private void OnCollisionEnter2D(Collision2D other) 
    {
        if(other.gameObject.name.Contains("Ball"))
        {
            
            if(!indestructible) --lives;
            if(lives == 0) Destroy(gameObject);
            if(!indestructible && lives == 0)
            {
                if(brick_spawner.reportBrickDeath())
                {

                   ball_behavior ball = FindObjectOfType<ball_behavior>();
                    ball.transform.position = new Vector3(0,-6.0f,0);
                }  

            }
             float random = Random.Range(0,1.0f);
             if(random < 0.3f && lives == 0 && balls.getBallNumber() == 1) {
                powerup.summon(transform.position.x,transform.position.y);
            }    
        }
        
        
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject.name.Contains("Bullet"))
        {
            if(!indestructible) --lives;
            if(lives == 0) Destroy(gameObject);
            if(!indestructible && lives == 0)
            {
                if(brick_spawner.reportBrickDeath())
                {

                    ball_behavior ball = FindObjectOfType<ball_behavior>();
                    ball.resetSounds();
                    ball.resetSpeed();
                    ball.transform.position = new Vector3(0,-6.0f,0);
                }  
            }
        }
    }


    public int getLives(){return this.lives;}

    public void setLives(int l){this.lives = l;}

    public void setIndestructible(bool i){this.indestructible = i;}

    public bool isIndestructible(){return this.indestructible;}

    
}
