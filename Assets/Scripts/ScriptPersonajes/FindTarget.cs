using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class FindTarget : MonoBehaviour{

    [SerializeField] private float detectionRange;
    [SerializeField] private float outsideRange;
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
            if (collider2D.CompareTag("Enemy") && colliderList.Contains(collider2D)){
                detectedEnemy = true;
                collider2D.GetComponent<SpriteRenderer>().material.color = new Color(1, 0.5f, 0.5f);
            }
            if (collider2D.CompareTag("Enemy") && !colliderList.Contains(collider2D)){
                detectedEnemy = false;
                collider2D.GetComponent<SpriteRenderer>().material.color = Color.white;
            }
        print(collider2D);
        }
        
    }
}
