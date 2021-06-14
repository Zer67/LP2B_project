using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class paddle_behavior : MonoBehaviour
{
    /* References */
    private paddle_behavior paddle_copy;
    private bullet_spawner bullet_script;
    private powerups_spawner powerup;

    /* Variables */
    public float speed;
    private float limit = 5.17f;
    public float ball_speed;
    private bool fireBullets = false;
    protected AudioSource sound_source;
    public AudioClip[] sounds;

    /* Constants */
    private readonly Vector3 BULLET_SPAWNING_LEFT_OFFSET = new Vector3(-0.55f,0.4f,0);
    private readonly Vector3 BULLET_SPAWNING_RIGHT_OFFSET = new Vector3(0.55f,0.4f,0);

    // Start is called before the first frame update
    void Start()
    {
        powerup = FindObjectOfType<powerups_spawner>();
        sound_source = gameObject.AddComponent<AudioSource>();
        sound_source.volume = 0.1f;
        sound_source.loop = false;
        sound_source.playOnAwake = false;
        bullet_script = FindObjectOfType<bullet_spawner>();
        paddle_copy = Instantiate(this);
        paddle_copy.gameObject.SetActive(false);
    }

    /**********************************************************************************/

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKey(KeyCode.RightArrow) && transform.position.x < limit){
            transform.Translate(speed*Time.deltaTime,0,0);
        } else if(Input.GetKey(KeyCode.LeftArrow) && transform.position.x > -limit){
            transform.Translate(-speed*Time.deltaTime,0,0);
        }
        // Return to menu if the player press the escape button
        if(Input.GetKey(KeyCode.Escape))
            StartCoroutine(returnToMenu());

        if(fireBullets)
        {
            if(Input.GetKeyDown(KeyCode.Space))
            {
                bullet_script.spawnBullets(transform.position + BULLET_SPAWNING_LEFT_OFFSET);
                bullet_script.spawnBullets(transform.position + BULLET_SPAWNING_RIGHT_OFFSET);
                sound_source.clip = sounds[0];
                sound_source.Play();
            }
        }
    }

   /**********************************************************************************/

    private void OnCollisionEnter2D(Collision2D other) {
        if(other.gameObject.name.Contains("Ball")){
            if(!powerup.isBallSticky())
            {
                float diffX = other.transform.position.x - transform.position.x;
                Vector2 dir = new Vector2(diffX,1).normalized;
                other.gameObject.GetComponent<Rigidbody2D>().velocity = dir * ball_speed;
            }
        }
    }

    /**********************************************************************************/

    IEnumerator returnToMenu(){
        AsyncOperation asyncload = SceneManager.LoadSceneAsync("Menu");
        while(!asyncload.isDone){
            yield return null;
        } 
    }

    /**********************************************************************************/
    public void reset()
    {
        this.canFireBullets(false);
        this.transform.localScale = paddle_copy.transform.localScale;
        this.ball_speed = paddle_copy.ball_speed;
    }

    /**********************************************************************************/

    public void setBallSpeed(float s){this.ball_speed = s;}

    public void setPaddleSpeed(int s){this.speed = s;}

    public void canFireBullets(bool b){this.fireBullets = b;}
}
