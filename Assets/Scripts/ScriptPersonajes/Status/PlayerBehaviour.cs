using Mochi.CharacterStats;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBehaviour : MonoBehaviour
{
    public PlayerStatusData Data;

    public CharacterStat Health;
    public CharacterStat Stamina;
    public CharacterStat Strength;
    public CharacterStat Technike;
    public CharacterStat Dexterity;
    public CharacterStat Constitution;

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
        Data.health.BaseValue -= damage;
        if (Data.health.BaseValue <= 0)
            {
            Destroy(gameObject);
            }
        }
    }
