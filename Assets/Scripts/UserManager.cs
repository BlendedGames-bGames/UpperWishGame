using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;


public class UserManager : MonoBehaviour
{
    // Start is called before the first frame update
    public string inputName;
    public string inputPassword;
    public string name;
    public string password; 
    public void Login()
    {
        StartCoroutine(GetText());
    }
    


    IEnumerator GetText() {
        //string name = "Gerardo";
        //string password = "asd123";
        name=inputName;
        password=inputPassword;
         Debug.Log("Se apreto Login");
        using(UnityWebRequest www = UnityWebRequest.Get($"localhost:3010/player/{name}/{password}"))
        {
        yield return www.SendWebRequest();
        Debug.Log("llego");
        if(www.isNetworkError || www.isHttpError) {
            Debug.Log("holi");
            Debug.Log(www.error);
        }
        else {
            // Show results as text
            Debug.Log("holo");
            Debug.Log(www.downloadHandler.text);
 
            // Or retrieve results as binary data
            byte[] results = www.downloadHandler.data;
            }
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
