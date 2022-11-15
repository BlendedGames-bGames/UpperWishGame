using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PantallaCompleta : MonoBehaviour
{
    // Start is called before the first frame update
    public Toggle toggle;
    private bool estadoToggle;

    void Start()
    {
        if(Screen.fullScreen){
            toggle.isOn=true;
            estadoToggle=false;
        }
        else{
            toggle.isOn=false;
            estadoToggle=true;
        }
    }

    // Update is called once per frame
    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape)){
            if(estadoToggle==false){
               
                toggle.isOn=false;
                estadoToggle=true;

            }
            else{
                toggle.isOn=true;
                estadoToggle=false;
            }
        
        
        }

        

    }

    public void ActivarPantallaCompleta(bool pantallaCompleta){
        Screen.fullScreen=pantallaCompleta;
    }
}
