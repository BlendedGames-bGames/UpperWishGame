using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ReiniciarJuego : MonoBehaviour
{
    // Start is called before the first frame update
   public void Reiniciar()
    {
        SceneManager.LoadScene("ScenaPrototipo1");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
