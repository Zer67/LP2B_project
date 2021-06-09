using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class paddle_behavior : MonoBehaviour
{

    private int score=0;
    public float speed;
    private float limit = 4.85f;

    public float ball_speed;

    public TextMeshPro score_displayer;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKey(KeyCode.RightArrow) && transform.position.x < limit){
            transform.Translate(speed*Time.deltaTime,0,0);
        } else if(Input.GetKey(KeyCode.LeftArrow) && transform.position.x > -limit){
            transform.Translate(-speed*Time.deltaTime,0,0);
        }
    }

    public void updateScore(int update){
        score+=update;
        score_displayer.SetText("Score : "+score);
    }

    public void ball_fall(){
        if(score - 500 > 0){
            score -= 500;
        } else {
            score = 0;
        }
        score_displayer.SetText("Score : "+score);
    }

    private void OnCollisionEnter2D(Collision2D other) {
        if(other.gameObject.name == "Ball"){
            float diffX = other.transform.position.x - transform.position.x;
            Vector2 dir = new Vector2(diffX,1).normalized;
            other.gameObject.GetComponent<Rigidbody2D>().velocity = dir * ball_speed;
        }
        
    }
}
