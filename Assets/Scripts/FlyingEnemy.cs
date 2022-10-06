using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyingEnemy : MonoBehaviour
{

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

    private RaycastHit2D hit;
    // Start is called before the first frame update
    void Start()
    {
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
            var value = Random.Range(1,6);
            Debug.Log("se movio" + value); //manda un mensaje por consola
            if( value == 1)// se mueve al lado contrario solo horizontal
            {
                if( movHor != 0)
                {
                movHor = movHor *-1;
                movVer = 0;
                }
                else
                {
                    var value2 = Random.Range(1,2);
                    if ( value == 1)
                    {
                    movHor = -1;
                    movVer = 0;   
                    }
                    else
                    {
                    movHor = 1;
                    movVer = 0;     
                    }
                     
                }
                
            }

            if( value == 2)// se mueve hacia arriba
            {
                movHor = 0;
                movVer = 1;
            }

            if( value == 3)//se mueve hacia abajo
            {
                movHor = 0;
                movVer = -1;
            }

            if( value == 4)
            {
                movHor = movHor *-1;
                movVer = 1;
            }

            if( value == 5)
            {
                movHor = movHor*-1;
                movVer = -1;
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


}


