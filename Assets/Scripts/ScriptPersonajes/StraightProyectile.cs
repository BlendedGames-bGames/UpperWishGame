using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;


public class StraightProyectile : MonoBehaviour
{
    public BulletData Data;
    public Rigidbody2D proyectile; 


    private void Update() {

        transform.Translate(transform.right * Data.travelSpeed * Time.deltaTime, Space.World);
    }
    void OnTriggerEnter2D(Collider2D collision) {

        if (collision.gameObject.CompareTag("Enemy")) {

            Destroy(collision.gameObject);
            //collision.gameObject.SetActive(false);
        }
        Destroy(gameObject);
    }   
}