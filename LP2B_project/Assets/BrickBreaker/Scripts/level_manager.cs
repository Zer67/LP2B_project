using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class level_manager : MonoBehaviour
{
    /* References */
    public brickCreation_script spawner;
    public TextMeshPro round_text;

    /* Constants */
    const float FADE_DURATION = 5.0f;

    // Start is called before the first frame update
    void Start()
    {
        if(menu.levels) round_text.gameObject.SetActive(true);
        else round_text.gameObject.SetActive(false);
    }

    /**********************************************************************************/

    // Update is called once per frame
    void Update()
    {
        
    }

    /**********************************************************************************/

    //Loading manually generated levels
    public IEnumerator LoadFirstLevel()
    {
        spawner.generateFirstLevel();
        round_text.SetText("Round 1");

        while(round_text.color.a != 0)
        {
            float newValue = round_text.color.a - Time.deltaTime / FADE_DURATION;
            round_text.color = new Color(0.87f,0.4f,0.2f,newValue);   
            yield return null;
        }
            
    }

    /**********************************************************************************/

    public IEnumerator LoadSecondLevel()
    {
        spawner.generateSecondLevel();
        round_text.SetText("Round 2");
        round_text.color = new Color(0.87f,0.4f,0.2f,1); 

        while(round_text.color.a != 0)
        {
            float newValue = round_text.color.a - Time.deltaTime / FADE_DURATION;
            round_text.color = new Color(0.87f,0.4f,0.2f,newValue);   
            yield return null;
        }
            
    }

    /**********************************************************************************/

    public IEnumerator LoadThirdLevel()
    {
        spawner.generateRandomGrid();
        round_text.SetText("Round 3");
        round_text.color = new Color(0.87f,0.4f,0.2f,1); 

        while(round_text.color.a != 0)
        {
            float newValue = round_text.color.a - Time.deltaTime / FADE_DURATION;
            round_text.color = new Color(0.87f,0.4f,0.2f,newValue);   
            yield return null;
        }
            
    }
}
