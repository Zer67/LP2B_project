using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class menu_script : MonoBehaviour
{

    protected const float TRANSITION_DURATION = 1.8f;

    protected float timer = 0f;

    
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        //Quit application
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }
    }

    IEnumerator LoadScene(string scene_name){
        yield return null;

        while(timer < TRANSITION_DURATION){
            timer += Time.deltaTime;
            yield return null;
        }
        AsyncOperation asyncload = SceneManager.LoadSceneAsync(scene_name);

        while(!asyncload.isDone){
            yield return null;
        }
    }

    public void setACscene(){
        StartCoroutine(LoadScene("AppleCatcher"));
    }

    public void setFBscene(){
        StartCoroutine(LoadScene("FurapiBird"));
    }

    public void setBBscene(){
        StartCoroutine(LoadScene("BrickBreaker"));
    }

}
