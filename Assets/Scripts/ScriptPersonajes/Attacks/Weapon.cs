using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public Transform firePoint;

    [SerializeField] private Transform magic_bulletPrefab;

    public GameObject TargetCursor;
  

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frames
    void Update() {

        if (Input.GetMouseButtonDown(0)) {

            ShootProyectile();
        }
        
    }

    void ShootProyectile() {

        Vector2 shootDir = TargetCursor.transform.position - transform.position;

        float angle = Vector3.Angle(Vector3.right, shootDir);

        if(TargetCursor.transform.position.y < transform.position.y) {
            angle *= -1;
        }
        Quaternion bulletRotation = Quaternion.AngleAxis(angle, Vector3.forward);

        Instantiate(magic_bulletPrefab, firePoint.position,bulletRotation);

    } 

}
