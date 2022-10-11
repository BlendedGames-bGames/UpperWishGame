using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RayCastProyectile : MonoBehaviour
{

    public static void Laser(Vector3 firePoint, Vector3 bulletRotation) {
        RaycastHit2D raycastHit2D = Physics2D.Raycast(firePoint, bulletRotation);

        if (raycastHit2D.collider != null) {
            // Aqui se mete el daño
            Debug.Log(raycastHit2D.transform.name);
        }
    }
}
