using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using System.Text;


public class UserManager : MonoBehaviour
{
    // Start is called before the first frame update
    public string inputName;
    public string inputPassword;
    public string name;
    public string password; 
    public GameObject reiniciar;
    public GameObject user;
    public GameObject bgames;
    public GameObject mensajeError;

    public bool v;

    public void Login()
    {
        StartCoroutine(GetText());
    }
    

    void  Update(){
        

        if(string.IsNullOrEmpty(inputName) || string.IsNullOrEmpty(inputPassword)){
            v=false;
            reiniciar.SetActive(false);
        }
        else{
            v=true;
            reiniciar.SetActive(true);
        }
    }


    IEnumerator GetText() {
        //string name = "Gerardo";
        //string password = "asd123";
        name=inputName;
        password=inputPassword;
        //Debug.Log("Se apreto Login");
        using(UnityWebRequest www = UnityWebRequest.Get($"localhost:3010/player/{name}/{password}"))
        {
        yield return www.SendWebRequest();
        Debug.Log("llego");
        if(www.isNetworkError || www.isHttpError) {
            Debug.Log("holi");
             PlayerPrefs.SetInt("log",0);
             mensajeError.SetActive(true);
            Debug.Log(www.error);
        }
        else {
            // Show results as text
            Debug.Log("holo");
            Debug.Log(www.downloadHandler.text);
            user.SetActive(false);
            bgames.SetActive(true);
            PlayerPrefs.SetInt("log",1);
            // Or retrieve results as binary data
            byte[] results = www.downloadHandler.data;
            string idstring = Encoding.UTF8.GetString(results);
            PlayerPrefs.SetString("id",idstring);
            }
           
            
            PlayerPrefs.SetString("nameUser",name);
            PlayerPrefs.SetString("PassUser",password);
        }
    }
    
    public void lecturaName(string s){
        inputName=s;
        Debug.Log(inputName);

    }
    public void lecturaPassword(string s){
        inputPassword=s;
        Debug.Log(inputPassword);

    }
}
