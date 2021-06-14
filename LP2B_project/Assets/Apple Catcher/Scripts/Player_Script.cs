using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class Player_Script : MonoBehaviour
{

    //---------------------------------------------------------------------------------
    // ATTRIBUTES
    //---------------------------------------------------------------------------------
    public TextMeshPro displayed_text;

    public SpawnerScript ref_spawner;
    
    protected int score = 0;
    protected AudioSource ref_audioSource;

    public float bound;

    protected const float timerWait = 5f;
    protected Animator ref_animator;

    //---------------------------------------------------------------------------------
    // METHODS
    //---------------------------------------------------------------------------------
    // Start is called before the first frame update
    void Start()
    {
        ref_audioSource = GetComponent<AudioSource>();
        ref_audioSource.volume = 0.3f;
        ref_animator = GetComponent<Animator>();

        
    }

    // Update is called once per frame
    void Update()
    {

        //Manage movement speed and animations
        float newSpeed = 0;

        if (Input.GetKey(KeyCode.LeftArrow) && gameObject.transform.position.x > -bound)
        {
            newSpeed = -10f;
            ref_animator.SetBool("isForwards", false);
        }
        else if ( Input.GetKey(KeyCode.RightArrow) && gameObject.transform.position.x < bound)
        {
            newSpeed = 10f;
            ref_animator.SetBool("isForwards", true);
        }

        if(gameObject.transform.position.x > 9.5f || gameObject.transform.position.x < -9.5f){
            gameObject.transform.position = new Vector3(-gameObject.transform.position.x,gameObject.transform.position.y,gameObject.transform.position.z);
            displayed_text.gameObject.SetActive(false);
            ref_spawner.StopAnimations();
            StartCoroutine(badApple());
        }

        
        
        //Inform animator : Are we moving?
        ref_animator.SetBool("isMoving", newSpeed != 0);


        //Move with the speed found
        transform.Translate(newSpeed * Time.deltaTime, 0, 0);

        //We stop time if the spaceBar is pushed down
        if ( Input.GetKeyDown(KeyCode.Space) )
        {
            Time.timeScale = 0f;
        }
        else if ( Input.GetKeyUp(KeyCode.Space) )
        {
            Time.timeScale = 1.0f;
        }

        // Return to menu if the player press the escape button
        if(Input.GetKey(KeyCode.Escape))
            StartCoroutine(returnToMenu());
    }

    //React to a collision (collision start)
    void OnCollisionEnter2D( Collision2D col )
    {
        score++;
        displayed_text.SetText("Score : " + score);

        ref_audioSource.Play();
    }

    IEnumerator returnToMenu(){

        AsyncOperation asyncload = SceneManager.LoadSceneAsync("Menu");

        while(!asyncload.isDone){
            yield return null;
        }
        
    }

    public void EnlargeBorders(){
        bound = 10f;
    }


    IEnumerator badApple(){
        float timer = 0f;
        while(timer < timerWait){
            timer += Time.deltaTime;
            yield return null;
        }
        displayed_text.gameObject.SetActive(true);
        ref_spawner.restartAnimation();
        bound = 8.8f;

        yield return null;
    }
}
