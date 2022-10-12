using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GetPortal : MonoBehaviour
{
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            GetComponent<SpriteRenderer>().enabled = false;
            //gameObject.transform.GetChild(0).gameObject.SetActivate(true);
            Destroy(gameObject, 0.1f);
            //SceneManagement.LoadScene(SceneManagement.GetActiveScene().buildIndex + 1);
        }
    }

}
