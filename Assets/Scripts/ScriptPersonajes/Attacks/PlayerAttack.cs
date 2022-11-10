using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    private Camera m_camera;
    private Vector3 mouse_position;

    public WeaponData Data;
    public GameObject bullet;
    public Transform bulletTransform;
    public bool canFire;
    private float timer;

    // Start is called before the first frame update
    void Start()
    {
        m_camera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
    }

    // Update is called once per frame
    void Update()
    {
        #region MOUSE_AIM

        mouse_position = m_camera.ScreenToWorldPoint(Input.mousePosition);
        Vector3 rotation = mouse_position - transform.position;
        float rotation_Zaxis = Mathf.Atan2(rotation.y, rotation.x) * Mathf.Rad2Deg;

        transform.rotation = Quaternion.Euler(0, 0, rotation_Zaxis);

        #endregion

        #region TIMERS

        if (!canFire)
            {
            timer += Time.deltaTime;
            if(timer > Data.attackInputBufferTime)
                {
                canFire = true;
                timer = 0;
                }
            }

        #endregion


        #region INPUT_HANDLER
        if (Input.GetMouseButton(0) && canFire)
            {
            ShootWeapon();
            }

        #endregion

        }


    // Funcion que obtiene la posicion del mouse respecto a la camara principal
    // Y crea un vector que apunta a dicha posicion
    private void ShootWeapon()
        {
            Instantiate(bullet, bulletTransform.position, Quaternion.identity);
        }
}
