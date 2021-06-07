using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PipeObstacle_Script : MonoBehaviour
{
    const float pipeSpeed = 4f;
    const float despawn_posX = -12f;

    public Player_script pscript;

    public Sprite[] textures;

    // Start is called before the first frame update
    void Start()
    {
        float earlyPipe_y = gameObject.transform.parent.GetComponent<Pipe_generator>().earlyPipe_y;
        gameObject.name = "Obstacle";
        pscript = gameObject.transform.parent.GetComponent<Player_script>();
        
        Sprite focusedTexture = textures[Random.Range(0,textures.Length)];
        gameObject.transform.Find("Pipe Top").gameObject.GetComponent<SpriteRenderer>().sprite = focusedTexture;
        gameObject.transform.Find("Pipe Bottom").gameObject.GetComponent<SpriteRenderer>().sprite = focusedTexture;
        gameObject.transform.position = new Vector3(12f,Random.Range(earlyPipe_y-2f,earlyPipe_y+2f),0);
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate( -pipeSpeed * Time.deltaTime , 0, 0 );
        if (transform.position.x < despawn_posX)
        {
            Destroy(gameObject);
        }
    }

    private void OnCollisionEnter2D(Collision2D other) {
        Destroy(other.gameObject);
    }

    private void OnTriggerExit2D(Collider2D other) {
        pscript.updateScore();
    }

}
