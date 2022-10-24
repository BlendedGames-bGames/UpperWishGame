using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Bullet : MonoBehaviour
{


    public float velX = 2f;
    public float velY;
    Rigidbody2D rb;



    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        
    }

    // Update is called once per frame
    void Update()
    {
        
        
        rb.velocity = transform.right * velX;
        Destroy(gameObject, 5f);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("Daño al personaje"); //manda un mensaje por consola
            player.obj.getDamage(5);
        }
        Destroy(gameObject);
    }

    
}
 
