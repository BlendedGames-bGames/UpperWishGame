using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class MenuPausa : MonoBehaviour
{
    [SerializeField] private GameObject botonPausa;
    [SerializeField] private GameObject menuPausa;
    [SerializeField] private GameObject Atack;
    [SerializeField] private GameObject check;

    Scene scena;
   
    private bool juegoPausado=false;

    public void Pausa(){
        juegoPausado=true;
        Time.timeScale=0f;
        botonPausa.SetActive(false);
        menuPausa.SetActive(true);
        Atack.SetActive(false);
        check.SetActive(false);
    }


    private void Update(){
        if(Input.GetKeyDown(KeyCode.P)){
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
        Atack.SetActive(true);
        check.SetActive(true);
    }

    public void SalirJuego(){
        SceneManager.LoadScene("Menu");
    }

}
