using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
public class usePoints : MonoBehaviour
{
    string new_data = "1";
    string url = "localhost:3002/spend_attribute/";

    string idPlayer = "2";
    string idAttributes = "0";
    public void Start()
    {
        StartCoroutine(GetPoints());
    }
    


    IEnumerator GetPoints() {
        WWWForm form = new WWWForm();
        form.AddField("id_player",idPlayer);
        form.AddField("id_attributes",idAttributes);
        form.AddField("new_data",new_data);

        using(UnityWebRequest www = UnityWebRequest.Post(url,form))
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

