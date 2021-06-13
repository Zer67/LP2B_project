using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class bird_script : MonoBehaviour
{

    public Pipe_generator ref_script_pipe;
    private GameObject parent;
    private Animator ref_animator;

    public TextMeshPro countdown;

    private const float delay_fadeout = 4f;

    private AudioSource bird_source;
    private const float max_volume = 0.3f;

    public AudioClip bird_sound;

    public SpriteRenderer fader;


    protected const float start_timer = 2f;

    protected const float die_timer = 2f;
    protected float timer;

    private bool control_enabled = false;
    private Vector2 force = new Vector2(0,7f);

    private Vector3 direction = new Vector3(0,0,0);
    // Start is called before the first frame update
    void Start()
    {
        bird_source = gameObject.AddComponent<AudioSource>();
        bird_source.clip = bird_sound;
        bird_source.volume = 0f;



        parent = gameObject.transform.parent.gameObject;
        ref_animator = gameObject.GetComponent<Animator>();
        gameObject.GetComponent<Rigidbody2D>().freezeRotation = true;
        StartCoroutine(fadeOut());
    }

    // Update is called once per frame
    void Update()
    {   
        
        if(gameObject != null){
            
            if(Input.GetKeyDown(KeyCode.UpArrow) && control_enabled){
                
                gameObject.GetComponent<Rigidbody2D>().velocity = Vector3.zero;
                gameObject.GetComponent<Rigidbody2D>().AddForce(force, ForceMode2D.Impulse);
            }
            

            if(gameObject.transform.position.y < -20){
                gameObject.transform.parent.GetComponent<Pipe_generator>().playDeathJingle();
                Destroy(gameObject);
            }
        }
        Vector3 dir = gameObject.GetComponent<Rigidbody2D>().velocity + new Vector2(5,0);
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
    }

    private void OnCollisionEnter2D(Collision2D other) {
        gameObject.GetComponent<Collider2D>().enabled = false;
        gameObject.GetComponent<Rigidbody2D>().velocity += new Vector2(8f,0);
        StartCoroutine(DieEffect());
        ref_animator.SetTrigger("Dying");
        
        control_enabled = false;
    }

    IEnumerator fadeOut(){
        bird_source.Play();
        Time.timeScale = 0;
        float alpha = 1f;
        while(timer < delay_fadeout){
            timer+=Time.unscaledDeltaTime;
            bird_source.volume += max_volume*Time.unscaledDeltaTime/delay_fadeout;
            alpha -= Time.unscaledDeltaTime/delay_fadeout;
            fader.color = new Color(255,255,255,alpha);
            yield return null;
        }
        bird_source.Stop();
        Destroy(bird_source);
        ref_script_pipe.PlayMusic();
        StartCoroutine(StartGame());
        yield return null;
    }

    IEnumerator StartGame(){
        timer = 0;
        int count = 3;
        countdown.SetText(count.ToString());

        
        while(timer < start_timer){
            timer+= Time.unscaledDeltaTime;
            if(timer >= start_timer/3 && count ==3 ){
                count--;
                countdown.SetText(count.ToString());
            } else if(timer >= start_timer*2/3 && count ==2 ){
                count--;
                countdown.SetText(count.ToString());
            }
            yield return null;
        }
        Destroy(countdown);
        control_enabled = true;
        Time.timeScale = 1f;
        yield return null;
    }

    IEnumerator DieEffect(){
        timer = 0;
        Time.timeScale = 0.6f;
        while(timer < die_timer){
            timer+= Time.unscaledDeltaTime;
            yield return null;
        }

        Time.timeScale = 1f;
        yield return null;

    }


}
