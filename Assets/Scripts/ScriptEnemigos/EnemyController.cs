using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
public class EnemyController : MonoBehaviour
{
    //(SEBA): aca meto la lista de enemigos y todo lo que necesito para el script de target nuevo
    public static List<EnemyController> enemyList = new List<EnemyController>();
    public static List<EnemyController> GetEnemyList(){ return enemyList; }
    // se crea la lista y se crea la funcion publica para obtener lo que hay dentro de la lista de enemigos


    [SerializeField]
    int damage;
    [SerializeField]
    private float speed;
    [SerializeField]
    private int health;

    [SerializeField]
    private EnemyStats data;


    /*private float minX,maxX,minY,maxY; // variables para posiciones minimas de los puntos para reaparicion
    [SerializeField]
    private Transform[] puntos;//lista con puntos para respawn
    [SerializeField]
    private Transform[] enemigos;// lista con enemigos para respawn
    [SerializeField]
    private float tiempoEnemigos;// variable para el tiempo de aparicion de enemigos
    private float tiempoSiguienteEnemigo; //variable para comprobar cuando hacer la aparicion*/

    private void Awake()
        {
        enemyList.Add(this);
        }

    //private GameObject player;
    // Start is called before the first frame update
    void Start()
    {
       // player = GameObject.FindGameObjectWithTag("Player");
        SetEnemyValues();
        /*maxX = puntos.Max(punto => punto.position.x);
        minX = puntos.Min(punto => punto.position.x);
        maxX = puntos.Max(punto => punto.position.y);
        maxX = puntos.Min(punto => punto.position.y);

        Random.seed =  (int)System.DateTime.Now.Ticks;*/
    }

    // Update is called once per frame
    void Update()
    {
       /* tiempoSiguienteEnemigo+= Time.deltaTime;
        if(tiempoSiguienteEnemigo >= tiempoEnemigos)
        {
            tiempoSiguienteEnemigo = 0;
            // crear enemigo
            CrearEnemigo();
        }*/
    }


    void SetEnemyValues() // funcion que copia los valores de vida,dano y velocidad del objeto scriptable EnemyStats
    {
        health = data.hp;   
        damage = data.damage;
        speed = data.speed;
    }

    void CrearEnemigo()
    {
        //maxX = puntos.Max(puntos => puntos.position.x);
        //minX = puntos.Min(puntos => puntos.position.x);
        //maxX = puntos.Max(puntos => puntos.position.y);
        //maxX = puntos.Min(puntos => puntos.position.y);
        //int numeroEnemigo = Random.Range(0,enemigos.Length);
        //Vector2 posicion = new Vector2(Random.Range(minX,maxX),Random.Range(minY,maxY));
        //Instantiate(enemigos[numeroEnemigo],posicion,Quaternion.identity);
    }


    void OnCollisionEnter2D(Collision2D collision)// funcion para daño del jugador al colicionar con un enemigo
    {
        if(collision.gameObject.CompareTag("Player")) 
        {
            Player.obj.getDamage(damage);

        }
    }

    public void getDamage(int damage)//funcion para recibir daño, en este caso para el enemigo
    {
        health -= damage;
        if(health <= 0){
            
            enemyList.Remove(this);
            Destroy(gameObject);
            
        }
            
    }
    
}
