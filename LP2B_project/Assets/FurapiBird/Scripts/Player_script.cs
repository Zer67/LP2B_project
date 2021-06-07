using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Player_script : MonoBehaviour
{
    private GameObject bird_player;
    public TextMeshPro text;
    private int score = 0;
    private Vector2 force = new Vector2(0,7f);
    // Start is called before the first frame update
    void Start()
    {
        bird_player = gameObject.transform.Find("Player Bird").gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        if(bird_player != null){
            if(Input.GetKeyDown(KeyCode.UpArrow)){
                bird_player.GetComponent<Rigidbody2D>().velocity = Vector3.zero;
                bird_player.GetComponent<Rigidbody2D>().AddForce(force, ForceMode2D.Impulse); 
            }

            if(bird_player.transform.position.y < -20){
                Destroy(bird_player);
            }
        }
        
    }

    public void updateScore() {
        score++;
        text.SetText("Score : "+score);
    }
}
