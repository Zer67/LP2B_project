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

    private paddle_behavior paddle;

    // Start is called before the first frame update
    void Start()
    {
        paddle = FindObjectOfType<paddle_behavior>();
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
                brick_spawner.reportBrickDeath();
            }
             float random = Random.Range(0,1.0f);
             if(random < 0.3f && lives == 0 && balls.getBallNumber() == 1 && brick_spawner.getBricks() != 0) {
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
                brick_spawner.reportBrickDeath();
            }
        }
    }


    public int getLives(){return this.lives;}

    public void setLives(int l){this.lives = l;}

    public void setIndestructible(bool i){this.indestructible = i;}

    public bool isIndestructible(){return this.indestructible;}

    
}
