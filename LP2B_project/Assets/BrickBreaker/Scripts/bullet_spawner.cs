using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bullet_spawner : MonoBehaviour
{
    private bullet_behavior bullet_prefab;

    private const float BULLET_SPEED = 7.0f;
    // Start is called before the first frame update
    void Start()
    {
        bullet_prefab = Resources.Load<bullet_behavior>("Prefabs/Bullet");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void spawnBullets(Vector3 position)
    {
        bullet_behavior spawnedBullet = Instantiate(bullet_prefab,position, Quaternion.identity) as bullet_behavior;

        spawnedBullet.gameObject.name = "Bullet";
        Quaternion rotation = Quaternion.Euler(0, 0, 90);
        spawnedBullet.transform.rotation = rotation;

        Rigidbody2D ballBodyLeft = spawnedBullet.GetComponent<Rigidbody2D>();
        
        ballBodyLeft.velocity = new Vector2(0,BULLET_SPEED);


    }
}
