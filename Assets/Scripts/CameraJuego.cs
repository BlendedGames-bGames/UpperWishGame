using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraJuego : MonoBehaviour
{

    public Transform John;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (John != null)
        {
            Vector3 position = transform.position;
            position.x = John.position.x;
            transform.position = position;
        }
    
    }
}
