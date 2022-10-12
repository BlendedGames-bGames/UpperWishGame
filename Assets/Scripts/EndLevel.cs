using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndLevel : MonoBehaviour
{
    private void Update()
    {
        AllGetPortal();
    }

    public void AllGetPortal()
    {
        if (transform.childCount == 0)
        {
            //a
            Debug.Log("Awa de Owo");
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
    }
}