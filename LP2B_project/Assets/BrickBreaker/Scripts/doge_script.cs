using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class doge_script : MonoBehaviour
{

    // Start is called before the first frame update
    void Start()
    {
        gameObject.name = "doge_coin";
        gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(0,-5f);
    }

    // Update is called once per frame
    void Update()
    {
        if(transform.position.y < -10){
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D other) {
        Debug.Log("Something enter : "+other.gameObject.name);
        if(other.gameObject.name == "Paddle"){
            other.gameObject.GetComponent<paddle_behavior>().updateScore(100);
            Destroy(gameObject);
        }
    }

    

}
