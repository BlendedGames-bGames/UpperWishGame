using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Player : MonoBehaviour
{
    public static Player obj;

    public int health;
    //public bool isGrounded = false;
    //public bool isMoving = false;
    //public bool isInmune = false;

    public float speed = 5f; //velocidad del pj
    //public Vector2 speedx; Pruebas en caso de que la velocidad fuese un vector
    //public float jumpForce = 3f;  // fuerza con la que puede saltar
    //public float movHor; // //valor para el movimiento horizontal del personaje

    //public float inmmuneTimeCnt = 0f; // variables de inmmunidad
    //public float inmmuneTime = 0.5f;

    //public LayerMask groundLayer; // para saber cuando esta tocando el piso o no
    //public float radius = 0.3f; // esta y la siguiente para saber si el pj esta tocando realmente el piso o no
    //public float groundRayDist = 0.5f;

    [SerializeField]
    private PlayerStatusData data;

    private Rigidbody2D rb;
    private Animator anim;
    private SpriteRenderer spr;
    public GameObject gameover;
    int vidaActual;
    float vidaA;
    public Image barraDevida;
    int aux;

void Awake()
{
    obj = this;
}


    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        spr = GetComponent<SpriteRenderer>();
        Time.timeScale=1f;
        gameover.SetActive(false);
        health = (int)data.health.BaseValue;
        aux=PlayerPrefs.GetInt("vida");
        health=PlayerPrefs.GetInt("vida");
    
       

    }

    // Update is called once per frame
    void Update()
    {
       /* movHor = Input.GetAxisRaw("Horizontal");
        isMoving = (movHor != 0f);

        // transform.position es la posicion actual, dara verdadero o falso por si el pj esta tocando el piso o no.
        isGrounded = Physics2D.CircleCast(transform.position, radius, Vector3.down, groundRayDist, groundLayer);

        if(Input.GetKeyDown(KeyCode.Space))//esto es para poder saltar
            jump();

        anim.SetBool("isMoving",isMoving);
        anim.SetBool("isGrounded",isGrounded);


        flip(movHor);*/
        
        vidaA=(float)health;
        barraDevida.fillAmount= vidaA/10;

    }
    
    void FixedUpdate() //todo lo que esta en su interior funcionara independiente del framerate del juego, todo lo que tiene que ver con fisicas,va aca
    {
        //rb.velocity = new Vector2(movHor * speed, rb.velocity.y); // permite mover al personaje a traves del rigid body, esto es para el movimiento en x en factor de su velocidad

    }

   /* public void jump()
    {
        if (!isGrounded) return;
        rb.velocity = Vector2.up*jumpForce;

    }
    //hara un flip al personaje
    private void flip(float _xValue)
    {
        Vector3 theScale = transform.localScale;

        if (_xValue == -1)
        theScale.x = Mathf.Abs(theScale.x)*(-1);
        else
        if ( _xValue == 1 )
            theScale.x = Mathf.Abs(theScale.x);
        
        transform.localScale = theScale;
        
    }
    */
    public void getDamage(int damage)
    {
        health -= damage;
        vidaActual=health;
        PlayerPrefs.SetInt("vida",vidaActual);
        Debug.Log(health);
        if(health <= 0)
        {
            Destroy(gameObject);
            Reiniciar();
        }
            
    }

   /* public void addLive()//funcion que añade las vidas en caso de que no sobrepase el maximo permitido.
    {
        lives++;
        
        if(lives > Game.obj.maxLives)
            lives = Game.obj.maxLives;
    }

    void OnDestroy()
    {
        obj = null;
    }*/
    public void Reiniciar(){
        gameover.SetActive(true);
        Time.timeScale=0f;

    }
}
