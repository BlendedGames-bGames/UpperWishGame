using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuInicio : MonoBehaviour

{

   public string scena;
    public string nombre;

    public void Jugar(){
        scena= PlayerPrefs.GetString("f",nombre);

        if(string.IsNullOrEmpty(scena)){
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex +1);
        }
        else{
        //scena= PlayerPrefs.GetString("f",nombre);
        SceneManager.LoadScene(scena);
        }


        Time.timeScale=1f;

    }

    public void Salir(){
        Debug.Log("Salir");
        Application.Quit();
    }

    public void borrar(){
        
        PlayerPrefs.DeleteKey("f");
            
    }

     public void a(){
         scena= PlayerPrefs.GetString("f",nombre);
         if(string.IsNullOrEmpty(scena)){
            Debug.Log("Salir");

        }
        

            
    }
}
