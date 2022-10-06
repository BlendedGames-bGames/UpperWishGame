using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class MenuPausa : MonoBehaviour
{
    [SerializeField] private GameObject botonPausa;
    [SerializeField] private GameObject menuPausa;

    private bool juegoPausado=false;

    public void Pausa(){
        juegoPausado=true;
        Time.timeScale=0f;
        botonPausa.SetActive(false);
        menuPausa.SetActive(true);
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
    }

    public void SalirJuego(){
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex -1);
    }
}
