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
    private bool timerIsUp=false;
    private bool timerGOisUp=false;
    private static readonly float countDown = 3;
    private static readonly float countDownGameOver = 6.4f;
    private float timer = countDown;
    private Rigidbody2D body;
    // Start is called before the first frame update
    void Start()
    {
        sound_source = gameObject.AddComponent<AudioSource>();
        //sound_source.volume = 0.008f;
        sound_source.volume = 0.1f;
        sound_source.clip = sounds[2];
        body = GetComponent<Rigidbody2D>();
        timer = countDown;
        timerIsUp = true;
        sound_source.Play();
    }

    // Update is called once per frame
    void Update()
    {
        /* Prevent the ball to be blocked */
        if(body.velocity.y == 0 && body.velocity.x != 0 & transform.position.y > 0){
            body.velocity += new Vector2(0,1f);
        }

        /* Ball go outside of the screen */
        if(transform.position.y < -10 && !timerGOisUp){
            body.velocity = new Vector2(0,0);
            timer = countDownGameOver;
            bricks_script.playGameOver();
            timerGOisUp = true;
        }

        /* Wait player over jingle to be finished */
        if(timerGOisUp){
            if(timer > 0){
                timer -= Time.deltaTime;
            } else {
                timerGOisUp=false;
                resetPosition();
            }
        }

        /* Wiat the start jingle to be finished */
        if(timerIsUp){
            if(timer > 0){
                timer -= Time.deltaTime;
            } else {
                body.velocity += new Vector2(0,-ball_speed);
                Debug.Log("Speed : "+ball_speed);
                timerIsUp =false;
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
                }
            }
        }
        
        sound_source.Play();
        
    }

    private void resetPosition(){  
        body.velocity = new Vector2(0,0);
        sound_source.clip = sounds[2];
        sound_source.Play();
        transform.position = new Vector3(0,-2f,0);
        
        paddle_script.ball_fall();
        timerIsUp = true;
        timer = countDown;
    }

    public void summonDoge(float x, float y){
        GameObject newDogeCoin = Instantiate(ref_prefab_doge_coin);
        newDogeCoin.transform.position = new Vector3(x,y,-2);
        bricks_script.playDoge();
    }



}
