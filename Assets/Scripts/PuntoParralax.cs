using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using TMPro;

public class PuntoParralax : MonoBehaviour
{
    // Start is called before the first frame update
    public string idPlayer;
    private int puntos;
    int social;
    int fisica;
    int afectivo;
    int cognitivo;
    int linguistico;

    public TMP_Text textFisico;
    public TMP_Text textSocial;
    public TMP_Text textAfectivo;
    public TMP_Text textCognitivo;
    public TMP_Text textLinguistico;
    int log;
    
    public void Start(){

         StartCoroutine(GetPoints());    
    }


    IEnumerator GetPoints() {
         Debug.Log("Se consiguieron los atributos");
         idPlayer=PlayerPrefs.GetString("id");
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

            social=attributeList[0].data;
            fisica=attributeList[1].data;
            afectivo=attributeList[2].data;
            cognitivo=attributeList[3].data;
            linguistico=attributeList[4].data;
            }
    }
    
    public void Update(){
        
       
        textFisico.text=fisica.ToString();
        textSocial.text=social.ToString();
        textCognitivo.text=cognitivo.ToString();
        textAfectivo.text=afectivo.ToString();
        textLinguistico.text=linguistico.ToString();
    
    }
}
