using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;


public class StraightProyectile : MonoBehaviour
{
    public BulletData Data;
    public Rigidbody2D proyectile;

    private Vector3 mouse_position;
    private Camera m_camera;
    public float force;



    private void Start()
        {

        m_camera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
        proyectile = GetComponent<Rigidbody2D>();
        mouse_position = m_camera.ScreenToWorldPoint(Input.mousePosition);

        Vector3 direction = mouse_position - transform.position;
        Vector3 rotation = transform.position - mouse_position;
        proyectile.velocity = new Vector2(direction.x, direction.y).normalized * force;

        float angle = Mathf.Atan2(rotation.y, rotation.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, angle + 180);
        }

    private void Update() {

    }
    void OnTriggerEnter2D(Collider2D collision) {

        if (collision.gameObject.CompareTag("Enemy")) {

            Destroy(collision.gameObject);
            //collision.gameObject.SetActive(false);
        }
        Destroy(gameObject);
    }   
}