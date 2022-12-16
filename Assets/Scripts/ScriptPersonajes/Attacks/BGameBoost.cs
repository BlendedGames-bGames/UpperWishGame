using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.Networking;

public class BGameBoost : MonoBehaviour
{

    public bool canBoost;

    private float timer;
    private float originalStat;

    private string url = "localhost:3002/spend_attribute/";

    private string idPlayer;

    private string idAttributes = "1";
    private string new_data = "5";



    [SerializeField] private float cooldown;
    [SerializeField] private PlayerAttack currentWeapon;
    [SerializeField] private AudioSource BoostSFX;
    [SerializeField] private Animation anim;

    private PuntoParralax puntoParralax;

    void Start()
    {
        idPlayer = PlayerPrefs.GetString("id");
        originalStat = currentWeapon.Data.attackInputBufferTime;

    }

    // Update is called once per frame
    void Update()
    {

        #region TIMERS
        if (!canBoost)
            {
            timer += Time.deltaTime;
            if (timer > cooldown)
                {
                canBoost = true;
                timer = 0;
                currentWeapon.firerate = originalStat;
                }
            }

        #endregion

        #region INPUT_HANDLER
        if (Input.GetKeyDown(KeyCode.Q)){
            StartCoroutine(GetPoints());
            gameBoost();
            canBoost = false;
            }

        #endregion
        }

    private void gameBoost()
        {
        currentWeapon.firerate = 0.1f;
        BoostSFX.Play();
        anim.Play("landingFX");
        }


    public IEnumerator GetPoints()
        {
        WWWForm form = new WWWForm();
        form.AddField("id_player", idPlayer);
        form.AddField("id_attributes", idAttributes);
        form.AddField("new_data", new_data);

        using (UnityWebRequest www = UnityWebRequest.Post(url, form))
            {
            yield return www.SendWebRequest();
            Debug.Log("llego");
            if (www.isNetworkError || www.isHttpError)
                {
                Debug.Log("holi");
                Debug.Log(www.error);
                }
            else
                {
                // Show results as text
                Debug.Log("holo");
                Debug.Log(www.downloadHandler.text);

                // Or retrieve results as binary data
                byte[] results = www.downloadHandler.data;
                }
            }
        }
    }
