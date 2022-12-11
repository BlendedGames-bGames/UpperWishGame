using Newtonsoft.Json;
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
        using (UnityWebRequest www = UnityWebRequest.Get($"localhost:3001/player_all_attributes/{idPlayer}"))
            {
            yield return www.SendWebRequest();
            Debug.Log("llego");
            if (www.isNetworkError || www.isHttpError) {
                Debug.Log("Error de conexion");
                Debug.Log(www.error);
                }
            else {
                // Show results as text
                Debug.Log(www.downloadHandler.text);

                // Or retrieve results as binary data
                byte[] results = www.downloadHandler.data;
                }
            // Se transforma el json en un objeto bGamesAtributes
            List<BgamesAttritbutes> attributeList = new List<BgamesAttritbutes>();
            attributeList = JsonConvert.DeserializeObject<List<BgamesAttritbutes>>(www.downloadHandler.text);

            Debug.Log(attributeList[0].name + ": " + attributeList[0].data);
            Debug.Log(attributeList[1].name + ": " + attributeList[1].data);
            Debug.Log(attributeList[2].name + ": " + attributeList[2].data);
            Debug.Log(attributeList[3].name + ": " + attributeList[3].data);
            Debug.Log(attributeList[4].name + ": " + attributeList[4].data);
            }
    }
    
    

}
