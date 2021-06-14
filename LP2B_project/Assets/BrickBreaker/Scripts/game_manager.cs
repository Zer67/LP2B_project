using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
public class game_manager : MonoBehaviour
{
    /* References */
    private brickCreation_script brick_spawner;
    private paddle_behavior paddle;
    private level_manager level;
    public TextMeshPro score_displayer;

    /* Variables */
    private bool endOfGame = false;
    private bool gameStart = true;
    private int round = 1;
    private int score=0;

    /* Variables */
    private const int TOTAL_ROUNDS = 2;

    // Start is called before the first frame update
    void Start()
    {
        level = FindObjectOfType<level_manager>();
        paddle = FindObjectOfType<paddle_behavior>();
        brick_spawner = FindObjectOfType<brickCreation_script>();

        if(menu.levels) StartCoroutine(level.LoadFirstLevel()); //Levels Mode
        if(menu.training) brick_spawner.generateRandomGrid(); //Training Mode
    }

    // Update is called once per frame
    void Update()
    {
        // Return to menu if the player press the escape button
        if(Input.GetKeyDown(KeyCode.Escape))
            StartCoroutine(returnToMenu());

        if(menu.levels) //Manually generated levels
        {
            if(brick_spawner.getBricks() == 0)
            {
                ++round;
                endOfGame = true;
                if(round != TOTAL_ROUNDS + 1 ) gameStart = true;
                else StartCoroutine(returnToMenu());
                
                switch(round)
                {
                    case 2:
                        StartCoroutine(level.LoadSecondLevel());
                    break;
                }
            }
        }

        if(menu.training) //Random generated levels
        {
            if(brick_spawner.getBricks() == 0)
            {
                endOfGame = true;
                gameStart = true;
                brick_spawner.generateRandomGrid();
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

     public void updateScore(int update){
        score+=update;
        score_displayer.SetText("Score : "+score);
    }

    /**********************************************************************************/

    public void ball_fall(){
        if(score - 500 > 0){
            score -= 500;
        } else {
            score = 0;
        }
        score_displayer.SetText("Score : "+score);
    }

    /**********************************************************************************/

    public bool isEndOfGame(){return this.endOfGame;}

    public void setEndOfGame(bool s){this.endOfGame = s;}

    public bool isGameStarting(){return this.gameStart;}

    public void setGameStarting(bool s){this.gameStart = s;}
      
}
