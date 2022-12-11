using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Animacion : MonoBehaviour
{

    [SerializeField] private GameObject animacion;
     private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
           animacion.SetActive(true);

           Destroy(animacion, 4f);
        }
        
    }
    
}
