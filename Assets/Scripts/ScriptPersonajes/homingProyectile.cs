using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(PolygonCollider2D))]

public class HomingProyectile : MonoBehaviour{

    public Transform target;
    public float speed = 10f;

    private Rigidbody2D proyectile;

    // Se inicia el componente rigidBody
    void Start(){
        proyectile = GetComponent<Rigidbody2D>();
        target = GetComponentInChildren<TargetController>().selectedTarget;
    }

    // Update is called once per frame
    void Update(){
        Vector2 direction = (Vector2)target.position - proyectile.position;

        direction.Normalize();
        float rotation = Vector3.Cross(direction, transform.up).z;

        proyectile.angularVelocity = -rotation * speed;
        proyectile.velocity = transform.up * speed;
    }

    private void OnTriggerEnter2D(Collider2D collision) {

        //
        Destroy(gameObject);
    }

}
