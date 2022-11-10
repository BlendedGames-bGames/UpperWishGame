using Mochi.CharacterStats;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBehaviour : MonoBehaviour
{
    public PlayerStatusData Data;

    public int CurrentHealth;
    public CharacterStat Stamina;
    public CharacterStat Strength;
    public CharacterStat Technike;
    public CharacterStat Dexterity;
    public CharacterStat Constitution;

    // Start is called before the first frame update
    void Start()
    {
        CurrentHealth = (int)Data.health.BaseValue;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void getDamage(int damage)
        {
        CurrentHealth -= damage;

        Debug.Log(damage);
        Debug.Log(CurrentHealth);
        if (CurrentHealth <= 0)
            {
            Destroy(gameObject);
            }
        }
    }
