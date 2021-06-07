using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class brick_behavior : MonoBehaviour
{

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        gameObject.name = "Brick";
    }

    private void OnCollisionEnter2D(Collision2D other) {
        if(other.gameObject.name == "Ball"){
            float random = Random.Range(0,1f);
            if(random < 0.1f) {
                other.gameObject.GetComponent<ball_behavior>().summonDoge(transform.position.x,transform.position.y);
            }
            Destroy(gameObject);
        }
    }
}
