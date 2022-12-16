using Microsoft.Win32.SafeHandles;
//using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeController : MonoBehaviour
{

    [SerializeField] int sec;

    public GameObject[] objects;

    private float remaining;
    //private bool inMove;
    //public bool spawnSpike = false;

    //PlayerMovement pm;

    void Update()
    {
        if (!Input.anyKeyDown)
        {
             remaining += Time.deltaTime;
             if (remaining > sec )
             {
                int rand = Random.Range(0, objects.Length);
                Instantiate(objects[rand], transform.position, Quaternion.identity);
                remaining = 0;
             }
        }
    }
}
