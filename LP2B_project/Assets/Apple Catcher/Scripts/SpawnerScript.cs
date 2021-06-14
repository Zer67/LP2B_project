using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class SpawnerScript : MonoBehaviour
{

    public AudioClip[] ref_audioClip;

    public SpriteRenderer fader_renderer;
    public Sprite white_apple;
    protected GameObject apple_prefab;
    protected float timer = 3f;

    protected float timerEndBadApple = 10f;

    protected bool stop = false;

    protected bool badApple = false;
    protected AudioSource ref_audioSource;
    protected float current_alpha = 1;

    // Start is called before the first frame update
    void Start()
    {
        
        apple_prefab = Resources.Load<GameObject>("Apple_prefab");

        ref_audioSource = gameObject.AddComponent<AudioSource>();
        ref_audioSource.loop = true;
        ref_audioSource.volume = 0.2f;
        ref_audioSource.clip = ref_audioClip[0];

        StartCoroutine( FadeOutFromWhite() );

    }

    // Update is called once per frame
    void Update()
    {
        timer -= Time.deltaTime;

        if ( timer <= 0 && !stop)
        {
            float randomX = Random.value * 17f - 8.5f;

            GameObject newApple = Instantiate(apple_prefab);
            newApple.transform.position = new Vector3(randomX, 6.0f, 0);
            if(badApple){
                newApple.GetComponent<SpriteRenderer>().sprite = white_apple;
                newApple.GetComponent<AppleScript>().multicolor = true;
            }
            timer = 0.5f + Random.value*1f ;
        }
        
    }

    //Coroutine to fade out from white/launch music with a delay
    IEnumerator FadeOutFromWhite()
    {
        yield return new WaitForSeconds(0.5f);

        ref_audioSource.Play();

        while (current_alpha > 0)
        {
            current_alpha -= Time.deltaTime / 2;
            fader_renderer.color = new Color(1, 1, 1, current_alpha);
            yield return null;
        }

    }

    public void StopAnimations(){
        stop = true;
        ref_audioSource.Stop();
    }

    public void restartAnimation(){
        ref_audioSource.clip = ref_audioClip[1];
        ref_audioSource.Play();
        stop = false;
        badApple = true;
        StartCoroutine(startVideoClip());
    }

    IEnumerator startVideoClip(){
        float timerBadApple = 0f;
        float total_timer = 0f;
        while(timerBadApple <timerEndBadApple){
            timerBadApple += Time.deltaTime;
            yield return null;
        }
        total_timer = timerBadApple;

        fader_renderer.gameObject.transform.position = new Vector3(0,0,0.0001f);

        while(current_alpha < 1f){
            current_alpha += Time.deltaTime / 2;
            fader_renderer.color = new Color(1,1,1,current_alpha);
            yield return null;
        }
        total_timer += current_alpha*2;
        fader_renderer.gameObject.GetComponent<VideoPlayer>().Play();
        fader_renderer.gameObject.GetComponent<VideoPlayer>().time = total_timer;



    }
}
