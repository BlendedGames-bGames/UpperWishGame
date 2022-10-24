using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/Enemy",order =1)]
public class EnemyStats : ScriptableObject
{
    public int hp;//vida del enemigo
    public int damage;//daño del enemigo
    public int speed;//velocidad del enemigo
}
