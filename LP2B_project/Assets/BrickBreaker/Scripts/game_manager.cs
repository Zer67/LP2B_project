using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class game_manager : MonoBehaviour
{
    private bool endOfGame = false;

    private bool gameStart = true;

    private brickCreation_script brick_spawner;

    private paddle_behavior paddle;

    private level_manager level;

    private int round = 1;
    // Start is called before the first frame update
    void Start()
    {
        level = FindObjectOfType<level_manager>();
        paddle = FindObjectOfType<paddle_behavior>();
        brick_spawner = FindObjectOfType<brickCreation_script>();
        if(menu.levels) StartCoroutine(level.LoadFirstLevel());
        
        if(menu.training) brick_spawner.generateRandomGrid();
    }

    // Update is called once per frame
    void Update()
    {
        // Return to menu if the player press the escape button
        if(Input.GetKeyDown(KeyCode.Escape))
            StartCoroutine(returnToMenu());

        if(menu.levels)
        {
            if(brick_spawner.getBricks() == 0)
            {
                ++round;
                endOfGame = true;
                if(round != 3 ) gameStart = true;
                switch(round)
                {
                    case 2:
                        StartCoroutine(level.LoadSecondLevel());
                    break;
                    /*case 3:
                        StartCoroutine(level.LoadThirdLevel());
                    break;*/
                }
            }
        }

        if(menu.training)
        {
            if(brick_spawner.getBricks() == 0)
            {
                endOfGame = true;
                gameStart = true;
                brick_spawner.generateRandomGrid();
            }
        }
    }

    public bool isEndOfGame(){return this.endOfGame;}

    public void setEndOfGame(bool s){this.endOfGame = s;}

    public bool isGameStarting(){return this.gameStart;}

    public void setGameStarting(bool s){this.gameStart = s;}



    IEnumerator returnToMenu(){

        AsyncOperation asyncload = SceneManager.LoadSceneAsync("Menu");

        while(!asyncload.isDone){
            yield return null;
        }
        
    }
        
}
