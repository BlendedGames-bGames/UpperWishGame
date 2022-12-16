using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpikeFallDamage : MonoBehaviour
{

    void OnTriggerEnter2D(Collider collision)
    {
        if(collision.transform.CompareTag("Player"))
        {
            //Debug.Log("Daño al personaje"); //manda un mensaje por consola
            Destroy(collision.gameObject);
            //Player.obj.Reiniciar();
        }

    }
}
