using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class menu : MonoBehaviour
{
     /* Variables */
    public static bool training = true;
    public static bool levels = false;

    // Start is called before the first frame update
    void Start()
    {

    }

    /**********************************************************************************/

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
            StartCoroutine(SetMenu());
    }

    /**********************************************************************************/

    public void LoadTraining()
    {
        training = true;
        levels = false;
        StartCoroutine(SetTraining());
    }

    /**********************************************************************************/

    public void LoadLevels()
    {
        levels = true;
        training = false;
        StartCoroutine(SetLevels());
    }

    /**********************************************************************************/

    public void LoadMenu()
    {
        StartCoroutine(SetMenu());
    }

    /**********************************************************************************/
     IEnumerator SetTraining(){
        AsyncOperation asyncload = SceneManager.LoadSceneAsync("BrickBreaker");
        while(!asyncload.isDone){
            yield return null;
        }  
    }

    /**********************************************************************************/
     IEnumerator SetLevels(){
        AsyncOperation asyncload = SceneManager.LoadSceneAsync("BrickBreaker");
        while(!asyncload.isDone){
            yield return null;
        }  
    }

    /**********************************************************************************/

    IEnumerator SetMenu(){
        AsyncOperation asyncload = SceneManager.LoadSceneAsync("Menu");
        while(!asyncload.isDone){
            yield return null;
        }   
    }
}
