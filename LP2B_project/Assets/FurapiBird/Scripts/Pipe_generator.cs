using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Pipe_generator : MonoBehaviour
{
    public GameObject ref_pipe;

    public background_script bg;
    private float timer = 0;

    private const float max_volume_music = 0.15f;

    private float timerSpeed = 0;
    private AudioSource source_music;

    private AudioSource source_sound;

    public AudioClip[] clips;
    private float nextPipeTimer = 0;

    bool isDead = false;

    public float globalPipeSpeed = 4f;
    public List<GameObject> pipes = new List<GameObject>();


    public float earlyPipe_y = 0;
    // Start is called before the first frame update
    void Start()
    {
        /* Let's put some music */
        source_music = gameObject.AddComponent<AudioSource>();
        source_music.clip = clips[0];
        source_music.volume = 0f;
        source_music.loop = true;

        /* And then some sounds */
        source_sound = gameObject.AddComponent<AudioSource>();
        source_sound.clip = clips[2];
        source_sound.volume = 0.25f;
        source_sound.loop = false;

        nextPipeTimer = Random.Range(1f,2.5f);
        GameObject newPipe = Instantiate(ref_pipe);
        pipes.Add(newPipe);
        newPipe.transform.parent = gameObject.transform;
        earlyPipe_y = newPipe.transform.position.y;
    }

    // Update is called once per frame
    void Update()
    {
        if(!isDead){
            if(timer < nextPipeTimer*(globalPipeSpeed+0.4f)/globalPipeSpeed && !isDead){
                timer += Time.deltaTime;
            } else if(!isDead){
                timer = 0;
                nextPipeTimer = Random.Range(1f,2f);
                GameObject newPipe = Instantiate(ref_pipe);
                newPipe.GetComponent<PipeObstacle_Script>().pipeSpeed = globalPipeSpeed;
                pipes.Add(newPipe);
                newPipe.transform.parent = gameObject.transform;
            }

            if(timerSpeed < 15f){
                timerSpeed += Time.deltaTime;
            } else{
                timerSpeed = 0;
                globalPipeSpeed +=1f;
                bg.mountain_speed += 1f;
                bg.clouds_speed += 1f;
                foreach(GameObject pipe in pipes){
                    pipe.GetComponent<PipeObstacle_Script>().pipeSpeed = globalPipeSpeed;
                }
            }

        }
        // Return to menu if the player press the escape button
        if(Input.GetKey(KeyCode.Escape)){
            Time.timeScale = 1f;
            StartCoroutine(returnToMenu());
        }
            
        

    }

    // We play the death sound and we stop every pipe when the bird hit a pipe.
    public void playDeathSound(){
        isDead = true;
        foreach(GameObject pipe in pipes){
            pipe.GetComponent<PipeObstacle_Script>().isStop = true;
        }
        source_sound.Play();
    }

    // We play the death jingle and we stop everything on the screen when the player go out of the screen.
    public void playDeathJingle(){
        bg.isStopped = true;
        foreach(GameObject pipe in pipes){
            pipe.GetComponent<PipeObstacle_Script>().isStop = true;
        }
        isDead = true;
        source_music.clip = clips[1];
        source_music.loop = false;
        source_music.Play();
        // Return to menu if the player died
        StartCoroutine(returnToMenu());
    }

    // The coroutine to return to menu when the player died & when the player pressed escape. 
    IEnumerator returnToMenu(){
        while(source_music.isPlaying && isDead){
            yield return null;
        }

        AsyncOperation asyncload = SceneManager.LoadSceneAsync("Menu");

        while(!asyncload.isDone){
            yield return null;
        }
        
    }

    // A public method to start the coroutine PlayMusicCor().
    public void PlayMusic(){
        StartCoroutine(PlayMusicCor());
    }


    // This coroutine is there to make a little fade in of  music.
    IEnumerator PlayMusicCor(){
        source_music.Play();
        while(source_music.volume < max_volume_music){
            source_music.volume += Time.unscaledDeltaTime*0.1f;
            yield return null;
        }
        yield return null;     
    }
}
