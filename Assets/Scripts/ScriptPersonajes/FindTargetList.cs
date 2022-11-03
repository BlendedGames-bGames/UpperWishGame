using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FindTargetList : MonoBehaviour
{
    [SerializeField] private float range;

    public static List<EnemyController> detectedEnemies = new List<EnemyController> ();
    public static List<EnemyController> GetEnemyList() { return detectedEnemies; }

    // Update is called once per frame
    void Update()
    {
        foreach(EnemyController enemyController in EnemyController.GetEnemyList())
            {
            if(Vector3.Distance(transform.position, enemyController.transform.position) < range)
                {
                detectedEnemies.Add(enemyController);
                }
            else if (Vector3.Distance(transform.position, enemyController.transform.position) > range && detectedEnemies.Contains(enemyController) )
                {
                detectedEnemies.Remove(enemyController);
                } 
            }
        
    }
}
