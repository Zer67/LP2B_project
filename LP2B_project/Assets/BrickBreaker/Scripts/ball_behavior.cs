using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ball_behavior : MonoBehaviour
{

    /* References */
    public paddle_behavior paddle_script;
    public brickCreation_script bricks_script;

    public GameObject ref_prefab_doge_coin;

    protected AudioSource sound_source;
    public AudioClip[] sounds;

    /* Variables */
    public float ball_speed;
   
    private Rigidbody2D body;

    private bool deathPlaying = false;
    private bool introPlaying = true;


    /* Constants */
     private const float DEATH_COUNT_DOWN = 2.0f;
    private const float INTRO_COUNT_DOWN = 3.0f;
    private const float SCREEN_BOTTOM = -5.3f;
    // Start is called before the first frame update
    void Start()
    {
        sound_source = gameObject.AddComponent<AudioSource>();
        sound_source.volume = 0.1f;
        sound_source.loop = false;
        sound_source.playOnAwake = false;
        body = GetComponent<Rigidbody2D>();
        body.velocity = (new Vector2(0,-ball_speed)).normalized * ball_speed;
    }

    // Update is called once per frame
    void Update()
    {   
        //Preventing ball from being horizontally stuck
        if(body.velocity.y == 0 && body.velocity.x != 0 & transform.position.y > 0)
            body.velocity += new Vector2(0,1f);
        
        /* Ball goes outside of the screen */
        if(this.transform.position.y <= SCREEN_BOTTOM)
        {   
            if(deathPlaying)
            {
                deathPlaying = false;
                StartCoroutine(waitDeath(DEATH_COUNT_DOWN));
            }
            
            if(introPlaying)
            {
                introPlaying = false;
                resetPosition();
                StartCoroutine(waitIntro(INTRO_COUNT_DOWN));    
            }
        }

    }

    private void OnCollisionEnter2D(Collision2D other) {
        if(other.gameObject.name == "Paddle"){
            sound_source.clip = sounds[0];
        } else {
            sound_source.clip = sounds[1];
            if(other.gameObject.name == "Brick"){
                paddle_script.updateScore(50);
                if(bricks_script.reportBrickDeath()){
                    resetPosition();
                    StartCoroutine(waitIntro(INTRO_COUNT_DOWN));    
                }
            }
        }
        
        sound_source.Play();
    }

    private void resetPosition(){  
        body.velocity = new Vector2(0,0);
        transform.position = new Vector3(0,-2f,0);
        paddle_script.ball_fall();
    }

    public void summonDoge(float x, float y){
        GameObject newDogeCoin = Instantiate(ref_prefab_doge_coin);
        newDogeCoin.transform.position = new Vector3(x,y,-2);
        bricks_script.playDoge();
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
         body.velocity = (new Vector2(0,-ball_speed)).normalized * ball_speed;
         deathPlaying = true;
    }





}
