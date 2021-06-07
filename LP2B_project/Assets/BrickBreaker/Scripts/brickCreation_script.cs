using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class brickCreation_script : MonoBehaviour
{
    public GameObject ref_brick_prefab;

    protected AudioSource player;

    public AudioClip[] jingle;

    private static readonly float x_step = 12f/13f;
    private static readonly float y_step = 0.5f;

    private int brickCreated = 0;
    public float cap;

    // Start is called before the first frame update
    void Start()
    {
        player = gameObject.AddComponent<AudioSource>();
        player.clip = jingle[0];
        //gameOverPlayer.volume = 0.01f;
        player.volume = 0.1f;
        generateRandomGrid();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void generateRandomGrid(){
        float x = -6 + 6f/13f;
        float y = 3.5f;
        float random;
        while(y >= 0){
            while(x < 0){
                random = Random.Range(0,1f);
                if(random > cap - x/26f){
                    GameObject newBrick = Instantiate(ref_brick_prefab);
                    newBrick.transform.parent = gameObject.transform;
                    newBrick.transform.position = new Vector3(x, y, 0);
                    newBrick.GetComponent<Renderer>().material.color = generateRandomColor();
                    if(x < -0.5){
                        GameObject newBrick_sym = Instantiate(ref_brick_prefab);
                        newBrick_sym.transform.parent = gameObject.transform;
                        newBrick_sym.transform.position = new Vector3(-x,y,0);
                        newBrick_sym.GetComponent<Renderer>().material.color = generateRandomColor();

                        brickCreated+=2;
                    } else {
                        brickCreated++;
                    }
                    
                }
                x+= x_step;
            }
            x= -6 + 6f/13f;
            y-= y_step;
        }        
    }

    public bool reportBrickDeath(){
        brickCreated--;
        if(brickCreated == 0){
            generateRandomGrid();
            return true;
        }
        return false;
    }

    public void playGameOver(){
        player.clip = jingle[0];
        play();
    }

    public void playDoge(){
        player.clip = jingle[1];
        play();
    }

    public void play(){
        player.Play();
    }

    private Color generateRandomColor(){
        return new Color32((byte)Random.Range(100,255), (byte)Random.Range(100,255), (byte)Random.Range(100,255), 255);
    }
}
