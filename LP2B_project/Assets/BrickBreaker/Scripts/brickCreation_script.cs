using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class brickCreation_script : MonoBehaviour
{
    /* References */
    public GameObject ref_brick_prefab;
    

    /* Variables */
    protected AudioSource player;
    public AudioClip[] jingle;
    private int brickCreated = 0;
    public float cap;

    /* Constants */
    private static readonly float X_STEP = 12f/13f;
    private static readonly float Y_STEP = 0.5f;

    private readonly Color32 [] colors={
        new Color32((byte)160, (byte)160, (byte)150, (byte)255), //Silver
        new Color32((byte)255, (byte)153, (byte)153, (byte)255), //Red
        new Color32((byte)255, (byte)255, (byte)153, (byte)255), //Yellow
        new Color32((byte)153, (byte)204, (byte)255, (byte)255), //Blue
        new Color32((byte)255, (byte)204, (byte)153, (byte)255), //Orange
        new Color32((byte)204, (byte)255, (byte)153, (byte)255), //Green
        new Color32((byte)102, (byte)255, (byte)255, (byte)255), //Cyan
        new Color32((byte)255, (byte)255, (byte)255, (byte)255), //White
        new Color32((byte)194, (byte)155, (byte)12, (byte)255) //Gold
    };

    // Start is called before the first frame update
    void Start()
    {
        player = gameObject.AddComponent<AudioSource>();
        player.clip = jingle[0];
        player.volume = 0.1f;
    }

    /**********************************************************************************/

    // Update is called once per frame
    void Update()
    {
    }

    /**********************************************************************************/

    public void generateRandomGrid(){
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

                    //Setting silvers brick lives to 2
                    if(newBrick.GetComponent<Renderer>().material.color == new Color32((byte)160, (byte)160, (byte)150, (byte)255))
                        tmp_brick.setLives(2);
                    //Setting gold bricks to indestructible
                    if(newBrick.GetComponent<Renderer>().material.color == new Color32((byte)194, (byte)155, (byte)12, (byte)255))
                        tmp_brick.setIndestructible(true);

                    if(x < -0.5){
                        GameObject newBrick_sym = Instantiate(ref_brick_prefab);
                        newBrick_sym.GetComponent<Renderer>().material.color = newBrick.GetComponent<Renderer>().material.color;
                        newBrick_sym.transform.parent = gameObject.transform;
                        newBrick_sym.transform.position = new Vector3(-x,y,0);
                        var tmp_sym_brick = newBrick_sym.GetComponent<brick_behavior>();
                        
                        //Setting silvers brick lives to 2
                        if(newBrick_sym.GetComponent<Renderer>().material.color == new Color32((byte)160, (byte)160, (byte)150, (byte)255))
                            tmp_sym_brick.setLives(2);
                        //Setting gold bricks to indestructible
                        if(newBrick_sym.GetComponent<Renderer>().material.color == new Color32((byte)194, (byte)155, (byte)12, (byte)255))
                            tmp_sym_brick.setIndestructible(true);

                        if(!tmp_brick.isIndestructible()) brickCreated+=2;
                    } else {
                        if(!tmp_brick.isIndestructible()) brickCreated++;
                    }  
                }
                x+= X_STEP;
            }
            x= -6 + 6f/13f;
            y-= Y_STEP;
        }   
    }

    /**********************************************************************************/

    public void generateFirstLevel()
    {
        float x = -6 + 6f/13f;
        float y = 2.5f;
        int color = 0;
        while(y >= 0){
            while(x < 6){
                GameObject newBrick = Instantiate(ref_brick_prefab);
                newBrick.transform.parent = gameObject.transform;
                newBrick.transform.position = new Vector3(x, y, 0);
                newBrick.GetComponent<Renderer>().material.color = colors[color];

                var tmp_brick = newBrick.GetComponent<brick_behavior>();
                if(newBrick.GetComponent<Renderer>().material.color == new Color32((byte)160, (byte)160, (byte)150, (byte)255))
                    tmp_brick.setLives(2);
                ++brickCreated;
                x+= X_STEP;
            }
            ++color;
            x= -6 + 6f/13f;
            y-= Y_STEP;
        }   
    }

    /**********************************************************************************/

    public void generateSecondLevel()
    {
        float x = -6 + 6f/13f;
        float y = 3.5f;
        int color = 7;
        float threshold = y;
        while(x < 6){
            while(y >= -1.5f)
            {
                GameObject newBrick = Instantiate(ref_brick_prefab);
                newBrick.transform.parent = gameObject.transform;
                newBrick.transform.position = new Vector3(x, y, 0);
                newBrick.GetComponent<Renderer>().material.color = colors[color];
                ++brickCreated;
                y-= Y_STEP;
            }
            if(color > 1) --color;
            else color = 7;
            x += X_STEP;
            threshold -= 0.5f;
            y = threshold;;
        }   

        x = -6 + 6f/13f;
        y = -2.0f;
        while(x < 6)
        {
            GameObject newBrick = Instantiate(ref_brick_prefab);
            newBrick.transform.parent = gameObject.transform;
            newBrick.transform.position = new Vector3(x, y, 0);
            newBrick.GetComponent<Renderer>().material.color = colors[0];

            var tmp_brick = newBrick.GetComponent<brick_behavior>();
            tmp_brick.setLives(2);
            ++brickCreated;
             x+= X_STEP;
        }
    }

    /**********************************************************************************/

    public void reportBrickDeath(){
        brickCreated--;
    }

    /**********************************************************************************/

    public void playGameOver(){
        player.clip = jingle[0];
        play();
    }

    /**********************************************************************************/

    public void playDoge(){
        player.clip = jingle[1];
        play();
    }

    /**********************************************************************************/

    public void play(){
        player.Play();
    }

    /**********************************************************************************/

    private Color generateRandomColor(){
        int index = Random.Range(0,9);
        return colors[index];
    }

    /**********************************************************************************/

    public int getBricks(){return this.brickCreated;}
}
