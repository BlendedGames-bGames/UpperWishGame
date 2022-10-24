using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [SerializeField]
    int damage;
    [SerializeField]
    private float speed;
    [SerializeField]
    private int health;

    [SerializeField]
    private EnemyStats data;

    //private GameObject player;
    // Start is called before the first frame update
    void Start()
    {
       // player = GameObject.FindGameObjectWithTag("Player");
        SetEnemyValues();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void SetEnemyValues()
    {
        health = data.hp;   //GetComponent<Health>().SetHealth(data.hp,data.hp);
        damage = data.damage;
        speed = data.speed;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Player")) 
        {
            player.obj.getDamage(damage);

        }
    }

    /*void OnCollisionEnter2D(Collision2D collision)
    {
        // dañar al player
        if(collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("Daño al personaje"); //manda un mensaje por consola
            player.obj.getDamage(500);
        }

    }*/
}
