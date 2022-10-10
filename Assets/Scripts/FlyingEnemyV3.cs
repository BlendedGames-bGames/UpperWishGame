using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class FlyingEnemyV3 : MonoBehaviour
{
    private Rigidbody2D rb;
    public Transform objetivo;  //variable para el objetivo a perseguir
    public float speed;

    public bool debePerseguir; //variable para ver si debe perseguir o no

    public float time = 0;
    Vector3 originalPos; // variable que guarda la posicion inicial para que se reinicie el enemigo

    public double distancia; //que tan lejos se esta del objetivo
    public LayerMask groundLayer;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>(); 
        float posicionInicialX = transform.position.x;
        float posicionInicialY = transform.position.y;

         originalPos = new Vector3(transform.position.x,transform.position.y,transform.position.z);
    }

    // Update is called once per frame
    void Update()
    {
        float distanciaX = objetivo.position.x - transform.position.x;
        float distanciaY = objetivo.position.y - transform.position.y;
        double multDistancia = Math.Pow(distanciaX,2) *Math.Pow(distanciaY,2);
        distancia = Math.Sqrt(multDistancia);
        if(Math.Abs(distancia)< 0.8)
        {
            time = 3;
            debePerseguir = true;
            flip(distanciaX);
        }
        else
        {
            debePerseguir = false;
            time -= Time.deltaTime;
        }
        if(debePerseguir == true)
        {
            transform.position = Vector2.MoveTowards(transform.position,objetivo.position,speed * Time.deltaTime);                 // moveTowards: posicion actual, objetiva y velocidad
        }
        if(time < 0)
        {
           transform.position = originalPos;
        }
    }


    

    private void flip(float _xValue) //funncion para dar vuelta el sprite
    {
        Vector3 theScale = transform.localScale;

        if (_xValue > 0)
        theScale.x = Mathf.Abs(theScale.x)*(-1);
        else
        if ( _xValue < 0 )
            theScale.x = Mathf.Abs(theScale.x);
        
        transform.localScale = theScale;
    }



}
