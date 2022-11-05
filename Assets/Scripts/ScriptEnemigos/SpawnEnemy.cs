using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;
public class SpawnEnemy : MonoBehaviour
{

    private float minX,maxX,minY,maxY; // variables para posiciones minimas de los puntos para reaparicion
    [SerializeField]
    private Transform[] puntos;//lista con puntos para respawn
    [SerializeField]
    private Transform[] enemigos;// lista con enemigos para respawn
    [SerializeField]
    private float tiempoEnemigos;// variable para el tiempo de aparicion de enemigos
    private float tiempoSiguienteEnemigo; //variable para comprobar cuando hacer la aparicion

    [SerializeField]
    private int cantidadEnemigos;// variable para elegir cuantos enemigos apareceran desde el spawn
    private int enemigosFaltante;


    // Start is called before the first frame update
    void Start()
    {
        maxX = puntos.Max(punto => punto.position.x);
        minX = puntos.Min(punto => punto.position.x);
        maxY = puntos.Max(punto => punto.position.y);
        maxY = puntos.Min(punto => punto.position.y);

        UnityEngine.Random.seed =  (int)System.DateTime.Now.Ticks;
        enemigosFaltante = cantidadEnemigos;

       // if(Math.Abs(minY) < Math.Abs(maxY))
         //   maxY = minY;
        //if(Math.Abs(minX) < Math.Abs(maxX))
         //   maxX = minX;
    }

    // Update is called once per frame
    void Update()
    {
        tiempoSiguienteEnemigo+= Time.deltaTime;
        if(tiempoSiguienteEnemigo >= tiempoEnemigos && cantidadEnemigos> 0)
        {
            tiempoSiguienteEnemigo = 0;
            // crear enemigo
            CrearEnemigo();
            cantidadEnemigos-= 1;
        }
    }


    void CrearEnemigo()
    {
        //maxX = puntos.Max(puntos => puntos.position.x);
        //minX = puntos.Min(puntos => puntos.position.x);
        //maxX = puntos.Max(puntos => puntos.position.y);
        //maxX = puntos.Min(puntos => puntos.position.y);
        int numeroEnemigo = UnityEngine.Random.Range(0,enemigos.Length);
        Vector2 posicion = new Vector2(maxX,maxY);
        Instantiate(enemigos[numeroEnemigo],posicion,Quaternion.identity);
    }

}
