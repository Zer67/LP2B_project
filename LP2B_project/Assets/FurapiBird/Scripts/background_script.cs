using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class background_script : MonoBehaviour
{
    public bool isStopped = false;
    public float mountain_speed;
    public float clouds_speed;
    private GameObject mountains;
    private GameObject clouds;
    // Start is called before the first frame update
    void Start()
    {
        mountains = gameObject.transform.Find("Mountains").gameObject;
        clouds = gameObject.transform.Find("Clouds").gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        
        if(!isStopped){
            /* We move each cloud and mountains to the left */
            mountains.transform.Translate(-mountain_speed*Time.deltaTime,0,0);
            clouds.transform.Translate(-clouds_speed*Time.deltaTime,0,0);
            float y = 0;
            float z = 0;
            /* We check if one cloud is outside the screen */
            for(int i=0;i<clouds.transform.childCount;i++){
                if(clouds.transform.GetChild(i).gameObject.transform.position.x < -9 -clouds.transform.GetChild(i).gameObject.GetComponent<SpriteRenderer>().bounds.size.x/2){
                    /* Then we put it to the right and we change his vertical position */
                    y = Random.Range(0,4f);
                    z = clouds.transform.GetChild(i).gameObject.transform.position.z;
                    clouds.transform.GetChild(i).gameObject.transform.position = new Vector3(9 +clouds.transform.GetChild(i).gameObject.GetComponent<SpriteRenderer>().bounds.size.x/2,y,z);
                }
            }
            /* In the same way we check if one mountain is outside the screen */
            for(int i=0;i<mountains.transform.childCount;i++){
                if(mountains.transform.GetChild(i).gameObject.transform.position.x < -9 -mountains.transform.GetChild(i).gameObject.GetComponent<SpriteRenderer>().bounds.size.x/2){
                    /* The we put it to the right */
                    y = mountains.transform.GetChild(i).gameObject.transform.position.y;
                    z = mountains.transform.GetChild(i).gameObject.transform.position.z;
                    mountains.transform.GetChild(i).gameObject.transform.position = new Vector3(9 +mountains.transform.GetChild(i).gameObject.GetComponent<SpriteRenderer>().bounds.size.x/2,y,z);
                }
            }
        }
        
    }
}
