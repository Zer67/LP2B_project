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

    private bool endOfGame = false;

    private Color32 [] colors={
        new Color32((byte)255, (byte)100, (byte)100, (byte)255), //Red
        new Color32((byte)100, (byte)255, (byte)100, (byte)255), //Green
        new Color32((byte)100, (byte)100, (byte)255, (byte)255), //Blue
        new Color32((byte)0, (byte)204, (byte)255, (byte)255), //Cyan
        new Color32((byte)255, (byte)255, (byte)255, (byte)255), //White
        new Color32((byte)255, (byte)133, (byte)51, (byte)255), //Orange
        new Color32((byte)255, (byte)255, (byte)51, (byte)255), //Yellow
        new Color32((byte)192, (byte)192, (byte)192, (byte)255), //Silver
        new Color32((byte)255, (byte)200, (byte)0, (byte)255) //Gold
    };
    // Start is called before the first frame update
    void Start()
    {
        player = gameObject.AddComponent<AudioSource>();
        player.clip = jingle[0];
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
                    var tmp_brick = newBrick.GetComponent<brick_behavior>();
                    if(tmp_brick.GetComponent<Renderer>().material.color == new Color32((byte)192, (byte)192, (byte)192, (byte)255))
                        tmp_brick.setLives(2);
                    if(tmp_brick.GetComponent<Renderer>().material.color == new Color32((byte)255, (byte)200, (byte)0, (byte)255))
                        tmp_brick.setIndestructible(true);
                    if(x < -0.5){
                        GameObject newBrick_sym = Instantiate(ref_brick_prefab);
                        newBrick_sym.GetComponent<Renderer>().material.color = newBrick.GetComponent<Renderer>().material.color;
                        newBrick_sym.transform.parent = gameObject.transform;
                        newBrick_sym.transform.position = new Vector3(-x,y,0);
                        var tmp_sym_brick = newBrick_sym.GetComponent<brick_behavior>();
                        
                        if(tmp_sym_brick.GetComponent<Renderer>().material.color == new Color32((byte)192, (byte)192, (byte)192, (byte)255))
                            tmp_sym_brick.setLives(2);
                        if(tmp_sym_brick.GetComponent<Renderer>().material.color == new Color32((byte)255, (byte)200, (byte)0, (byte)255))
                            tmp_sym_brick.setIndestructible(true);

                        if(!tmp_brick.isIndestructible()) brickCreated+=2;
                    } else {
                        if(!tmp_brick.isIndestructible()) brickCreated++;
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
        int index = Random.Range(0,9);
        return colors[index];
    }

    public bool isEndOfGame(){return this.endOfGame;}
}
