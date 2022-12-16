using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class TerrainDamage : MonoBehaviour
{

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.CompareTag("Player"))
        {
            //Debug.Log("Da�o al personaje"); //manda un mensaje por consola
            Destroy(collision.gameObject);
            Player.obj.Reiniciar();
        }

    }

}
