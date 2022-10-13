using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class FindTarget : MonoBehaviour{

    [SerializeField] private float detectionRange;
    [SerializeField] private float outsideRange;

    public List<Collider2D> targetList = new List<Collider2D>();
    public bool detectedEnemy;

    // Start is called before the first frame update
    void Start(){
        detectedEnemy = false;
    }

    // Update is called once per frame
    void Update(){
        Collider2D[] colliderList = Physics2D.OverlapCircleAll(transform.position, detectionRange);
        Collider2D[] outsideList = Physics2D.OverlapCircleAll(transform.position, outsideRange);
        foreach (Collider2D collider2D in outsideList) {
            if (collider2D.CompareTag("Enemy") && colliderList.Contains(collider2D))  {
                detectedEnemy = true;
                if (!targetList.Contains(collider2D)){
                    targetList.Add(collider2D);
                }
            }
            else if (collider2D.CompareTag("Enemy") && !colliderList.Contains(collider2D))  { 
                detectedEnemy = false;
                if (targetList.Contains(collider2D)) {
                    targetList.Remove(collider2D);
                }
            }
        }
    }

}
