using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KuroWeapon : MonoBehaviour
{
    public Transform firePoint;
    public GameObject magic_bulletPrefab;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update() {

        if (Input.GetMouseButtonDown(0)) {
            Shoot();
        }
        
    }

    void Shoot() {
    
        Instantiate(magic_bulletPrefab, firePoint.position, firePoint.rotation);
    } 
}
