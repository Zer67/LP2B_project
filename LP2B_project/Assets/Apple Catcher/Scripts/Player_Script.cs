using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Player_Script : MonoBehaviour
{

    //---------------------------------------------------------------------------------
    // ATTRIBUTES
    //---------------------------------------------------------------------------------
    public TextMeshPro displayed_text;

    protected int score = 0;
    protected AudioSource ref_audioSource;
    protected Animator ref_animator;

    //---------------------------------------------------------------------------------
    // METHODS
    //---------------------------------------------------------------------------------
    // Start is called before the first frame update
    void Start()
    {
        ref_audioSource = GetComponent<AudioSource>();
        ref_animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {

        //Manage movement speed and animations
        float newSpeed = 0;

        if (Input.GetKey(KeyCode.LeftArrow))
        {
            newSpeed = -10f;
            ref_animator.SetBool("isForwards", false);
        }
        else if ( Input.GetKey(KeyCode.RightArrow) )
        {
            newSpeed = 10f;
            ref_animator.SetBool("isForwards", true);
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

        //Quit game
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }
    }

    //React to a collision (collision start)
    void OnCollisionEnter2D( Collision2D col )
    {
        score++;
        displayed_text.SetText("Score : " + score);

        ref_audioSource.Play();
    }

}
