using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Turret : MonoBehaviour
{
    public bool movimiento;
    Animator anim;
    int disparoID;
    private float movHor = 1; 
    public double distancia; //que tan lejos se esta del objetivo
    public Transform objetivo;  //variable para el objetivo a perseguir
    public Transform Torreta; // sera para saber su rotacion;
    public GameObject Bullet;
    public float time = 1;
     public bool disparo;
     public int comprobar; // variable para comprobar si movimiento viene de estar atacando o estar a distancia, si es 1, atacando,si es 2 es distancia
    // Start is called before the first frame update
    void Start()
    {
        movimiento = true;
        anim = GetComponent<Animator>();
        disparoID = Animator.StringToHash("disparar");
    }

    void FixedUpdate()
    {
        double distanciaX = objetivo.position.x - transform.position.x;
        double distanciaY = objetivo.position.y - transform.position.y;
        double multDistancia = Math.Pow(distanciaX,2) *Math.Pow(distanciaY,2);
        distancia = Math.Sqrt(multDistancia);

        if(distancia <= 1.28)
        {
            if(comprobar == 2)
            {
                movimiento = true;
                comprobar = 1;
            }
            if(movimiento)
            {
                if(time == 1)
                    time -= 1;
                else if(time == 0)
                {
                    time = 1;
                    movHor = movHor*(-1);
                }
                flip(movHor);
                movimiento = false;
                anim.SetBool(disparoID,movimiento);
                Invoke("DispararProyectil",0.4f);
                Invoke("DispararProyectil",0.55f);
                Invoke("DispararProyectil",0.7f);
                Invoke("Moverse",3);
                
                
            }

        }
        else
        {
            movimiento = false;
            anim.SetBool(disparoID, movimiento);
            comprobar = 2;
        }
    }

    void DispararProyectil()
    {
        Instantiate(Bullet, Torreta.position,Torreta.rotation);

    }

    

    void Moverse()
    {
        movimiento = true;
        anim.SetBool(disparoID, movimiento);
    }

    private void flip(float _xValue)
    {
        Vector3 theScale = transform.localScale;

        if (_xValue == -1)
        {
            //theScale.x = Mathf.Abs(theScale.x)*(-1);
            transform.eulerAngles = new Vector3(0,180,0);
        }
        else
        if ( _xValue == 1 )
        {
            //theScale.x = Mathf.Abs(theScale.x);
            transform.eulerAngles = new Vector3(0,0,0);
        }
        
        transform.localScale = theScale;
        
    }

    
}
