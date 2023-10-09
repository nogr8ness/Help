using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private Projectile[] spikes;
    
    private Rigidbody2D body;

    private void Awake()
    {
        body = GetComponent<Rigidbody2D>();
        RandomizeSpeed();
        print(Screen.width);
        print(Screen.height);
    }

    private void Update()
    {
        body.velocity = new Vector2(-speed, 0);

        if(transform.position.x < -8.5)
        {
            ResetPosition();
            Player.score++;
        }
    }

    private void ResetPosition()
    {
        transform.position = new Vector3(8.5f, transform.position.y, transform.position.z);
        RandomizeSpeed();
        
    }

    private void RandomizeSpeed()
    {
        speed = Random.Range(3.5f, 10f);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        print(collision.gameObject.name);
        if(collision.gameObject.tag == "Player")
        {
            foreach(Projectile spike in spikes)
            {
                spike.ResetPosition();
            }
        }
    }
}
