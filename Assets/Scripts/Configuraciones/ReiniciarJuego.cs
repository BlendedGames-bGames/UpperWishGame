using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ReiniciarJuego : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private GameObject botonPausa;
     [SerializeField] private GameObject barra;
    Player player= new Player();
    public void Reiniciar()
    {
        PlayerPrefs.SetInt("vida",player.health);
        SceneManager.LoadScene("Starting_Scene");

        
    }

    // Update is called once per frame
    void Start()
    {
      
        botonPausa.SetActive(false);
        barra.SetActive(false);
    
        
    
    }

    public void SalirJuego(){
        SceneManager.LoadScene("Menu");
    }
}
