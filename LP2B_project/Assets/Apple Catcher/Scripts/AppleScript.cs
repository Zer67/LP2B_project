using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AppleScript : MonoBehaviour
{
    public bool multicolor = false;

    protected byte red = 1;

    protected int redToAdd = 1;
    protected byte blue = 1;
    protected int blueToAdd = 1;
    protected byte green = 1;
    protected int greenToAdd = 1;


    // Start is called before the first frame update
    void Start()
    {
        red = (byte) Random.Range(0,200);
        blue = (byte) Random.Range(0,200);
        green = (byte) Random.Range(0,200);
    }

    // Update is called once per frame
    void Update()
    {
        // This section to make a smooth multicolor apple in the bad apple mod.
        if(multicolor){
            gameObject.GetComponent<SpriteRenderer>().color = new Color32(red,blue,green,255);
            red+=(byte)redToAdd;
            blue+=(byte)blueToAdd;
            green+=(byte)greenToAdd;

            if(Mathf.Abs(red) > 255){
                redToAdd*=-1;
                red += (byte)redToAdd;
            }

            if(Mathf.Abs(blue) > 255){
                blueToAdd*=-1;
                blue += (byte)blueToAdd;
            }

            if(Mathf.Abs(green) > 255){
                greenToAdd*=-1;
                green += (byte)greenToAdd;
            }
        }
        if (transform.position.y < -10.0f)
        {
            Destroy(gameObject);
        }
    }

    //React to a collision (collision start)
    void OnCollisionEnter2D(Collision2D col)
    {
        Destroy(gameObject);
    }
}
