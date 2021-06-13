using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bullet_behavior : MonoBehaviour
{
    private paddle_behavior paddle;
    
    // Start is called before the first frame update
    void Start()
    {
        paddle = FindObjectOfType<paddle_behavior>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter2D(Collider2D other) {
        if(other.gameObject.name.Contains("Brick") || other.gameObject.name.Contains("Wall"))
        {
            paddle.updateScore(50);
            Destroy(gameObject);
        }
    }
}
