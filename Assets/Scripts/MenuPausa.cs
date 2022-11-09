using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class MenuPausa : MonoBehaviour
{
    [SerializeField] private GameObject botonPausa;
    [SerializeField] private GameObject menuPausa;
    [SerializeField] private GameObject botonJuego;
    [SerializeField] private GameObject menuJuego;
    Scene scena;
   

    private bool juegoPausado=false;

    public void Pausa(){
        juegoPausado=true;
        Time.timeScale=0f;
        botonPausa.SetActive(false);
        menuPausa.SetActive(true);
        botonJuego.SetActive(false);
    }

    public void Juego(){
        juegoPausado=true;
        Time.timeScale=0f;
        botonJuego.SetActive(false);
        menuJuego.SetActive(true);
        botonPausa.SetActive(false);
    }

    private void Update(){
        if(Input.GetKeyDown(KeyCode.G)){
            if(juegoPausado){
                Reanudar();
            }
            else{
                Pausa();
            }
        }


    }

     public void Reanudar(){
        juegoPausado=false;

        Time.timeScale=1f;
        botonPausa.SetActive(true);
        menuPausa.SetActive(false);
        botonJuego.SetActive(true);
        menuJuego.SetActive(false);
    }

    public void SalirJuego(){
        SceneManager.LoadScene("Menu");
    }

    public void Guardar(){
        scena= SceneManager.GetActiveScene();
        string nombre= scena.name;
        PlayerPrefs.SetString("f",nombre);
        Reanudar();
        botonJuego.SetActive(true);
        menuJuego.SetActive(false);
        botonPausa.SetActive(true);
            
    }

    public void borrar(){
        
        PlayerPrefs.DeleteKey("f");
         Reanudar();
        botonJuego.SetActive(true);
        menuJuego.SetActive(false);
        botonPausa.SetActive(true);
            
    }
}
