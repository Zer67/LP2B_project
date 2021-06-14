using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Player_script : MonoBehaviour
{
    private GameObject bird_player;

    
    public TextMeshPro text;
    private int score = 0;
    

    // Start is called before the first frame update
    void Start()
    {
        bird_player = gameObject.transform.Find("Player Bird").gameObject;
        
    }

    // Update is called once per frame
    void Update()
    {
        
        
    }

    // Just an update of the score called in the PipeObstacle_script.
    public void updateScore() {
        score++;
        text.SetText("Score : "+score);
    }

}
