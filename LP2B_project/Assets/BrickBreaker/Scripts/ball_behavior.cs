using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ball_behavior : MonoBehaviour
{

    /* References */
    private paddle_behavior paddle_script;
    private brickCreation_script bricks_script;


    protected AudioSource sound_source;
    public AudioClip[] sounds;

    /* Variables */
    public float y_ball_speed;
   
    private Rigidbody2D body;

    private bool deathPlaying = false;
    private bool introPlaying = true;

    private bool sticky = false;

    private ball_spawner spawner_script;

    private powerups_spawner powers_spawner;

    /* Constants */
     private const float DEATH_COUNT_DOWN = 2.0f;
    private const float INTRO_COUNT_DOWN = 3.0f;
    private const float SCREEN_BOTTOM = -5.3f;
    // Start is called before the first frame update
    void Start()
    {
        powers_spawner = FindObjectOfType<powerups_spawner>();
        spawner_script = FindObjectOfType<ball_spawner>();
        paddle_script = FindObjectOfType<paddle_behavior>();
        bricks_script = FindObjectOfType<brickCreation_script>();
        sound_source = gameObject.AddComponent<AudioSource>();
        sound_source.volume = 0.1f;
        sound_source.loop = false;
        sound_source.playOnAwake = false;
        body = GetComponent<Rigidbody2D>();   
    }

    // Update is called once per frame
    void Update()
    {   
        //Preventing ball from being horizontally stuck
        if(body.velocity.y >= -1 && body.velocity.y <= 1 &&  body.velocity.x != 0 & transform.position.y > 0)
        {

            Vector2 tmp = body.velocity + new Vector2(0,-1.0f);
            body.velocity = tmp.normalized * y_ball_speed;
        }
        
        /* Ball goes outside of the screen */
        if( this.transform.position.y <= SCREEN_BOTTOM)
        {   

            if(spawner_script.getBallNumber() > 1 ) 
            {
                spawner_script.decreaseBallNumber();
                spawner_script.removeBall(this);
                Destroy(this.gameObject);
            }
            else
            {
                if(deathPlaying)
                {
                    deathPlaying = false;
                    StartCoroutine(waitDeath(DEATH_COUNT_DOWN));
                }
                
                if(introPlaying)
                {
                    resetAll();
                    resetPosition();
                    StartCoroutine(waitIntro(INTRO_COUNT_DOWN));    
                }
            }
        }

            if( !introPlaying && body.velocity.x == 0 && body.velocity.y == 0 && Input.GetKeyDown(KeyCode.Space))
            {
                
                if(sticky) StartCoroutine(launchBall());
                else
                {
                    GetComponent<Rigidbody2D>().isKinematic = false;
                    transform.parent = null;
                    sticky = false;
                    body.velocity = (new Vector2(0,-y_ball_speed)).normalized * y_ball_speed;
                }
            }
        

    }

    private void OnCollisionEnter2D(Collision2D other) {
        if(other.gameObject.name == "Paddle")
        {
            if(sticky)
            {
                 GetComponent<Rigidbody2D>().isKinematic = true;
                body.velocity = Vector3.zero;
                transform.parent = paddle_script.transform;
            }
            else 
            {
                sound_source.clip = sounds[0];
                sound_source.Play();
            }
        } else if(other.gameObject.name == "Brick")
        {
            sound_source.clip = sounds[1];
            sound_source.Play();
            paddle_script.updateScore(50);
        }
 
        
}

    private void resetPosition(){  
        GetComponent<Rigidbody2D>().isKinematic = true;
        body.velocity = Vector3.zero;
        transform.position = paddle_script.transform.position + new Vector3(0,0.38f,0);
        transform.parent = paddle_script.transform;
        paddle_script.ball_fall();
    }

    

    IEnumerator waitDeath(float duration)
    {
        this.sound_source.clip = this.sounds[4];
         this.sound_source.Play();
        yield return new WaitForSeconds(duration);
        introPlaying = true;
    }
    

    IEnumerator waitIntro(float duration)
    {
        this.sound_source.clip = this.sounds[2];
        this.sound_source.Play();
         yield return new WaitForSeconds(duration);  
         paddle_script.setPaddleSpeed(11);
         deathPlaying = true;
         introPlaying = false;
    }

    IEnumerator launchBall( )
    {
        GetComponent<Rigidbody2D>().isKinematic = false;
        transform.parent = null;
        sticky = false;
        body.velocity = (new Vector2(0,-y_ball_speed)).normalized * y_ball_speed;
        yield return new WaitForSeconds(0.1f); 
        sticky = true;
    }
   

    public bool isSticky(){ return sticky;}

    public void setSticky(bool s){this.sticky = s;}

    public float getBallSpeed(){return this.y_ball_speed;}

    public void resetAll()
    {
        this.spawner_script.reset();
        this.paddle_script.reset();
        
        
        if(sticky)
        { 
            this.GetComponent<Rigidbody2D>().isKinematic = false;
            this.transform.parent = null;
            sticky = false;
            if(body.velocity.y == 0) body.velocity = (new Vector2(0,-y_ball_speed)).normalized * y_ball_speed;
        }
    }

    public void resetSounds()
    {
        deathPlaying = false;
        introPlaying = true;
    }

    public void resetSpeed()
    {
        body.velocity = Vector2.zero;
    }

}
