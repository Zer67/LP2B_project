using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class menu_script : MonoBehaviour
{

    protected const float TRANSITION_DURATION = 2.8f;

    protected AudioSource ambiance_sound;

    protected AudioSource selection_sound;

    public AudioClip[] clips;

    protected float timer = 0f;

    
    // Start is called before the first frame update
    void Start()
    {
        ambiance_sound = gameObject.AddComponent<AudioSource>();
        selection_sound = gameObject.AddComponent<AudioSource>();

        ambiance_sound.clip = clips[0];
        selection_sound.clip = clips[1];

        ambiance_sound.volume = 0.1f;
        selection_sound.volume = 0.2f;

        ambiance_sound.loop = true;
        ambiance_sound.Play();
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
        selection_sound.Play();

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
