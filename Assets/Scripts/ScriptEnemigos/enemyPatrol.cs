using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyPatrol : MonoBehaviour
{

   // [HideInInspector]
    public bool mustPatrol;//variable para ver si debe moverse o no
    public float speed = 3f;//velocidad del enemigo
    private bool mustTurn; //variable para saber si debe girarse o no

    [SerializeField]
    private EnemyStats data;


    private Rigidbody2D rb;
    public Transform groundCheckPos; // variable para poder ver la posicion del objeto hijo groundCheck
    public LayerMask groundLayer;
    public Collider2D bodyCollider;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>(); 
        mustPatrol = true;
        speed = data.speed;
    }

    // Update is called once per frame
    void Update()
    {
        if(mustPatrol)
        {
            Patrol();
        }
    }

    void FixedUpdate()
    {
        if(mustPatrol)
        {
            mustTurn = !Physics2D.OverlapCircle(groundCheckPos.position,0.1f,groundLayer); // revisa hacia en un radio de un circulo si existe piso o no
        }

        
    }

    void Patrol()
    {
        if (mustTurn || bodyCollider.IsTouchingLayers(groundLayer))
        {
            
            flip();
        }
        rb.velocity = new Vector2(speed*Time.fixedDeltaTime, rb.velocity.y);
    }


    private void flip() //funncion para dar vuelta el sprite
    {
       
        mustPatrol = false;
        
        transform.localScale = new Vector2(transform.localScale.x * -1,transform.localScale.y);
        speed *= -1;
        mustPatrol = true;
    }
}
