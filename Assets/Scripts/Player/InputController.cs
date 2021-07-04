using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputController : MonoBehaviour
{
    [SerializeField]
    private PlayerController controller;

    public float inputHorizontal;
    public float inputVertical;

    public bool jumpPressed;


    void Start()
    {
        // Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        GetInput();
    }

    void OnJump() {
        jumpPressed = true;
        controller.movement.OnJump();
    }

    void OnLand() {
        jumpPressed = false;
    }

    void OnSprint() {
        controller.movement.OnSprint();
        // controller.animation.OnSprint();
    }

    void OnWalk() {
        controller.movement.OnWalk();
        // controller.animation.OnWalk();
    }

    void OnIdle() {
        // controller.animation.OnIdle();
    }

    void OnAttack() {
    }

    void GetInput() {

        if (Input.GetButtonUp("Jump")) {
            OnLand();
        }

        if (Input.GetButtonDown("Jump")) {
            OnJump();
        }

        if (Input.GetButtonDown("Walk")) {
            OnWalk();
        }

        if (Input.GetButtonUp("Walk")) {
            OnSprint();
        }

        if (Input.GetButtonDown("Attack")) {
            // if (controller.hitPoints != 0f) {
            //     Cursor.lockState = CursorLockMode.Locked;
            // }
            OnAttack();
        }

        inputHorizontal = Input.GetAxis("Horizontal");
        inputVertical = Input.GetAxis("Vertical");

        if ( inputHorizontal == 0 && inputVertical == 0 ) {
            OnIdle();
        } else if ( (inputHorizontal != 0 || inputVertical != 0) && !Input.GetButton("Walk") ) {
            OnSprint();
        }
    }
}
