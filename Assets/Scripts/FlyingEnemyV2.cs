using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyingEnemyV2 : MonoBehaviour
{

// falta mejorar colicion con enemigo.
  private Rigidbody2D rb;
    public float movHor = 0f;
    public float speed = 3f;
    public float movVer = 0f;
    public bool isGroundFloor = true;
    public bool isGroundFront = false;

    public LayerMask groundLayer;
    public float frontGrndRayDist = 0.25f;
    public float floorCheckY = 0.52f;
    public float frontCheck = 0.51f;
    public float frontDist = 0.001f;

    public int scoreGive = 50;

    public int patron;
    public float movAnt;// variablñe que guarda el sentido del mov horizontal anterior
    public int value = 1; // variable que dice que empieza de forma horizontal
    private RaycastHit2D hit;
    // Start is called before the first frame update
    void Start()
    {
        var Patron = Random.Range(1,3);// valor random para el patron a utilizar
        Debug.Log(Patron);
        if(Patron == 1)
        {
            movHor = 1;
            patron = 1;
            movAnt = 1;
            flip(movHor,-1);
        }
        else
        {
            movHor = -1;
            patron = 2;
            movAnt = -1;
        }

        
        rb = GetComponent<Rigidbody2D>(); 
    }

    // Update is called once per frame
    void Update()
    {
        //evitar caer en un precipicio
        /*isGroundFloor =(Physics2D.Raycast(new Vector3(transform.position.x, transform.position.y - floorCheckY, transform.position.z )
        , new Vector3(movHor, 0, 0), frontGrndRayDist, groundLayer));// revisa la distancia en x en 1 hacia delante si se mueve en esa direccion o 1 hacia atras en caso contrario

        if (isGroundFloor)//mientras encuentre piso no hara nada
            movHor = movHor * -1;
        */


        
            // Choque con pared
        if (Physics2D.Raycast(transform.position, new Vector3(movHor, movVer, 0), frontCheck, groundLayer)) //si el raycast da verdadero con el choque,cambiaremos el sentido del mov
        {
            Debug.Log("entro 1");
            if(patron == 1)
            {
                Debug.Log("llego" + patron);
                if( value == 1)// se mueve vertical arriba izquierda
                {
                    
                    movHor = -1;
                    movVer = 1;     
                    value = 2;
                    flip(movHor,-1);
                    movAnt = movHor;
                }
                    

                else if( value == 2)// se mueve hacia abajo
                {
                    movHor = 0;
                    movVer = -1;
                    value = 3;
                    flip(movHor,movAnt);
                    movAnt = movHor;
                }

                else if( value == 3)//se mueve vertical arriba derecha
                {
                    movHor = 1;
                    movVer = 1;
                    value = 4;
                    flip(movHor,-1);
                    movAnt = movHor;
                }

                else if( value == 4) // se mueve vertical inferior izquierda
                {
                    movHor = -1;
                    movVer = -1;
                    value = 5;
                    flip(movHor,-1);
                    movAnt = movHor;
                }

                else if( value == 5) // se mueve hacia arriba
                {
                    movHor = 0;
                    movVer = 1;
                    value = 6;
                    flip(movHor,movAnt);
                    movAnt = movHor;
                }

                else if( value == 6) // se mueve hacia inferior izquierda
                {
                    movHor = -1;
                    movVer = -1;
                    value = 7;
                    flip(movHor,-1);
                    movAnt = movHor;
                }

                else if( value == 7) // se mueve hacia izquierda
                {
                    movHor = -1;
                    movVer = 0;
                    value = 8;
                    flip(movHor,-1);
                    movAnt = movHor;
                }

                else if( value == 8) // se mueve hacia derecha
                {
                    movHor = 1;
                    movVer = 0;
                    value = 1;
                    flip(movHor,-1);
                    movAnt = movHor;
                }

            }
            else
            {
                Debug.Log("llego" + patron);
                if( value == 1)// se mueve vertical arriba derecha
                {
                    movHor = 1;
                    movVer = 1;     
                    value = 2;
                    flip(movHor,-1);
                    movAnt = movHor;
                }
                    

                else if( value == 2)// se se mueve vertical abajo derecha
                {
                    movHor = 1;
                    movVer = -1;
                    value = 3;
                    flip(movHor,-1);
                    movAnt = movHor;
                }

                else if( value == 3)//se mueve vertical arriba izquierda
                {
                    movHor = 1;
                    movVer = 1;
                    value = 4;
                    flip(movHor,-1);
                    movAnt = movHor;
                }

                else if( value == 4) // se mueve arriba
                {
                    movHor = 0;
                    movVer = 1;
                    value = 5;
                    flip(movHor,movAnt);
                    movAnt = movHor;
                }

                else if( value == 5) // abajo vertical izquierda
                {
                    movHor = -1;
                    movVer = -1;
                    value = 6;
                    flip(movHor,-1);
                    movAnt = movHor;
                }

                else if( value == 6) // se mueve hacia abajo
                {
                    movHor = 0;
                    movVer = -1;
                    value = 7;
                    flip(movHor,movAnt);
                    movAnt = movHor;
                }

                else if( value == 7) // se mueve hacia derecha
                {
                    movHor = 1;
                    movVer = 0;
                    value = 8;
                    flip(movHor,-1);
                    movAnt = movHor;
                }

                else if( value == 8) // se mueve hacia izquierda
                {
                    movHor = -1;
                    movVer = 0;
                    value = 1;
                    flip(movHor,-1);
                    movAnt = movHor;
                }
            }
        
        }
            
            

    

        
        
           
        //Choque con otro enemigo
        hit = (Physics2D.Raycast(new Vector3(transform.position.x + movHor*frontCheck, transform.position.y + movVer*frontCheck, transform.position.z ),
            new Vector3(movHor, 0, 0), frontDist));// revisa la distancia en x en 1 hacia delante si se mueve en esa direccion o 1 hacia atras en caso contrario

        
        if (hit != null)
        {
            if (hit.transform != null)
            {
                if(hit.transform.CompareTag("Enemy"))//revisa si choca con el enemigo
                    movHor = movHor *-1;
            }
        }
        

        

    }

    void FixedUpdate()
    {
        rb.velocity = new Vector2(movHor*speed, movVer *speed);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        // dañar al player
        if(collision.gameObject.CompareTag("Player"))
        {
            //Debug.Log("Daño al personaje"); //manda un mensaje por consola
            player.obj.getDamage();
        }

    }

    private void flip(float _xValue, float movAnt)
    {
        Vector3 theScale = transform.localScale;

        if (_xValue != movAnt)
            theScale.x = Mathf.Abs(theScale.x)*(-1);
        else
            theScale.x = Mathf.Abs(theScale.x);
        
        transform.localScale = theScale;
        
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        // Destruye al enemigo
        if(collision.gameObject.CompareTag("Player"))
        {
            getKilled();
        }

    }

    private void getKilled()
    {
         gameObject.SetActive(false); // funcion que desactiva el objeto
    } 



}
    
/*
    public bool horizontal;
    public float velocidad;
    public float longitud;

    float contador;// referencia para saber en que punto esta del movimiento
    float posicionInicial;
    float posicionActual;
    float PosicionAnterior;

    FlyingEnemy enemy;

    void start()
    {
        if(horizontal)
        {
            posicionInicial = transform.position.x;

        }
        else
        {
            posicionInicial = transform.position.y;
        }
        enemy = GetComponent<FlyingEnemy>();
    }
*/






