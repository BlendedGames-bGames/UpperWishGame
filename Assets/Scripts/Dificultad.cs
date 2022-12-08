using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Dificultad : MonoBehaviour
{

    public GameObject flecha;
    public float time;
    [SerializeField] private GameObject botonDificultad;
    [SerializeField] private GameObject menuDificultad;
    [SerializeField] private GameObject Atack;
    [SerializeField] private GameObject check;
    [SerializeField] private GameObject facil;
    [SerializeField] private GameObject dificil;
    int dificultad;

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
        //PlayerPrefs.SetInt("Gdificultad",1);
        InvokeRepeating("Dialogo",0f,time);
        int aux=PlayerPrefs.GetInt("Gdificultad",1);
        if(aux==1){
             facil.SetActive(true);
        }
        if(aux==0){
            dificil.SetActive(true);   
        }

    }

    // Update is called once per frame
    void Update()
    {
        
    }

//  Use this for initialization

 
     public void Dialogo(){
        flecha.SetActive(!flecha.activeInHierarchy);
    }

    public void ModoFacil(){
        PlayerPrefs.SetInt("Gdificultad",1);
        PlayerPrefs.SetInt("vida",20);
        facil.SetActive(true);
        dificil.SetActive(false);
        dificultad=1;
         Volver();
    }

    public void ModoDificil(){
        PlayerPrefs.SetInt("vida",10);
        PlayerPrefs.SetInt("Gdificultad",0);
        facil.SetActive(false);
        dificil.SetActive(true);
        dificultad=0;
         Volver();
    }
}
