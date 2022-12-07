using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dificultad : MonoBehaviour
{

    public GameObject flecha;
    public float time;
    [SerializeField] private GameObject botonDificultad;
    [SerializeField] private GameObject menuDificultad;
    [SerializeField] private GameObject Atack;
    [SerializeField] private GameObject check;

    public void Pausa(){
        
        Time.timeScale=0f;
        botonDificultad.SetActive(false);
        menuDificultad.SetActive(true);
        Atack.SetActive(false);
        check.SetActive(false);
    }

    public void Volver(){
        Time.timeScale=1f;
        botonDificultad.SetActive(true);
        menuDificultad.SetActive(false);         
        Atack.SetActive(true);
        check.SetActive(true);
    }
   

    public void Start(){
        PlayerPrefs.SetInt("vida",20);
        InvokeRepeating("Dialogo",0f,time);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

     public void Dialogo(){
        flecha.SetActive(!flecha.activeInHierarchy);
        

    }

    public void ModoFacil(){
         PlayerPrefs.SetInt("vida",20);
         Volver();
    }

    public void ModoDificil(){
         PlayerPrefs.SetInt("vida",10);
         Volver();
    }
}
