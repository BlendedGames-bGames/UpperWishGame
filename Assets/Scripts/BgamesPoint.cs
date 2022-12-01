using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class BgamesPoint : MonoBehaviour
{

// Start is called before the first frame update
    public string idPlayer = "0";
 

    public void Start()
    {
        StartCoroutine(GetPoints());
    }
    


    IEnumerator GetPoints() {
         Debug.Log("Se consiguieron los atributos");
        using(UnityWebRequest www = UnityWebRequest.Get($"localhost:3001/player_all_attributes/{idPlayer}"))
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
    
    

}
