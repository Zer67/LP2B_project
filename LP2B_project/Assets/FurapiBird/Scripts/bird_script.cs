using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bird_script : MonoBehaviour
{
    private GameObject parent;
    private Animator ref_animator;


    protected const float start_timer = 2f;

    protected const float die_timer = 2f;
    protected float timer;

    private bool control_enabled = false;
    private Vector2 force = new Vector2(0,7f);

    private Vector3 direction = new Vector3(0,0,0);
    // Start is called before the first frame update
    void Start()
    {
        parent = gameObject.transform.parent.gameObject;
        ref_animator = gameObject.GetComponent<Animator>();
        gameObject.GetComponent<Rigidbody2D>().freezeRotation = true;
        StartCoroutine(StartGame());
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

    IEnumerator StartGame(){
        timer = 0;
        Time.timeScale = 0;
        while(timer < start_timer){
            timer+= Time.unscaledDeltaTime;
            yield return null;
        }
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
