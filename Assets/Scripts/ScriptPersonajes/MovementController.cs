using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementController : MonoBehaviour {

    public MovementActions controller;

    public float runSpeed = 40f;
    float horizontalMove = 0f;
    bool jump = false;
    bool crouch = false;

    // Update is called once per frame
    void Update(){

        horizontalMove = Input.GetAxisRaw("Horizontal") * runSpeed ;

        if (Input.GetButtonDown("Jump")) {
            jump = true;
        }

        if (Input.GetButtonDown("Crouch")) {
            jump = true;
        } else if (Input.GetButtonUp("Crouch")) {
                crouch = false;
            }

    }
    void FixedUpdate() {

        // Inputs de movimiento
        controller.Move(horizontalMove * Time.fixedDeltaTime, false, jump);
        jump = false;
    }
}
