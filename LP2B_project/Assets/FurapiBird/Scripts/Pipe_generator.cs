using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pipe_generator : MonoBehaviour
{
    public GameObject ref_pipe;
    private float timer = 0;
    private float nextPipeTimer = 0;

    public float earlyPipe_y = 0;
    // Start is called before the first frame update
    void Start()
    {
        nextPipeTimer = Random.Range(1f,2.5f);
        GameObject newPipe = Instantiate(ref_pipe);
        newPipe.transform.parent = gameObject.transform;
        earlyPipe_y = newPipe.transform.position.y;
    }

    // Update is called once per frame
    void Update()
    {
        if(timer < nextPipeTimer){
            timer += Time.deltaTime;
        } else {
            timer = 0;
            nextPipeTimer = Random.Range(1f,2f);
            GameObject newPipe = Instantiate(ref_pipe);
            newPipe.transform.parent = gameObject.transform;
        }
    }
}
