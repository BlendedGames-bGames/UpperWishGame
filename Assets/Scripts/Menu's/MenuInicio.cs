using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuInicio : MonoBehaviour

{
    Player player= new Player();

    public void Jugar(){
    SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex +1);
    PlayerPrefs.SetInt("vida",player.health);
    Time.timeScale=1f;
    }

    public void Salir(){
        Debug.Log("Salir");
        Application.Quit();
    }


        
}
