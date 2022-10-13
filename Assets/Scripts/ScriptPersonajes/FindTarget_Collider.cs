using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FindTarget_Collider : MonoBehaviour
{
    List<Collider2D> TargetList = new List<Collider2D>();
    
    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.CompareTag("Enemy")) {
            TargetList.Add(collision);
        }
    }

    private void OnTriggerExit2D(Collider2D collision) {
        if (collision.CompareTag("Enemy")) {
            TargetList.Remove(collision);
        }
    }
}

