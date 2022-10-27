using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBehaviour : MonoBehaviour
{
    public PlayerStatusData Data;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void getDamage(int damage)
        {
        Data.health -= damage;
        if (Data.health <= 0)
            {
            Destroy(gameObject);
            }
        }
    }
