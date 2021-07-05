using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementController : MonoBehaviour
{
    [SerializeField]
    internal PlayerController controller;

    [SerializeField]
    internal CollisionController collision;

    [Space(10)]
    public float walkSpeed = 6f;
    public float runSpeed = 10f;
    public float fallSpeed = 6f;
    public float jumpTime = 2f;

    [Space(5)]
    public Vector3 jumpVector;
    public float jumpForwardMultiplier = 15f;

    [Space(10)]
    public ForceMode jumpForceMode;
    public ForceMode fallForceMode;
    public ForceMode moveForceMode;

    public LayerMask layerMask;

    [Space(10)]
    public Vector3 moveDirection = Vector3.zero;
    public float currentFallSpeed = 0f;
    public bool jumping = false;
    public bool falling = false;

    internal Rigidbody rigidBody;
    internal float speed;
    internal CapsuleCollider characterCollider;
    internal bool stairForward;
    internal Transform cameraTransform;
    internal Vector3 rightAxis;
    internal Vector3 forwardAxis;


    public void OnJump() {
        if ( collision.isGrounded ) {
            jumping = true;
            controller.animation.OnJump();
            StartCoroutine(Jump());
        }
    }

    public void OnSprint() {
        speed = runSpeed;
    }

    public void OnWalk() {
        speed = walkSpeed;
    }


    void Start()
    {
        rigidBody = GetComponent<Rigidbody>();
        characterCollider = GetComponent<CapsuleCollider>();
        collision = GetComponent<CollisionController>();
        cameraTransform = controller.camera.camera.transform;
        OnWalk();
    }

    void Update() {
        UpdateAnimation();
        UpdateRotation();
    }

    void FixedUpdate()
    {
        moveDirection = Vector3.zero;

        rightAxis = cameraTransform.right;
        forwardAxis = cameraTransform.forward;
        rightAxis.y = 0;
        forwardAxis.y = 0;

        Vector3 userInput = controller.input.inputHorizontal * rightAxis + controller.input.inputVertical * forwardAxis;
        moveDirection += Vector3.ClampMagnitude(userInput, 1f);
        moveDirection *= speed;

        if ( collision.isGrounded ) {
            currentFallSpeed = 0f;
        } else {
            currentFallSpeed += Physics.gravity.y * fallSpeed;
            rigidBody.AddForce(Vector3.up * currentFallSpeed, fallForceMode);
        }

        if ( stairForward ) {
            rigidBody.AddForce(Vector3.up * 200f, jumpForceMode);
            rigidBody.AddForce(moveDirection * 100f, jumpForceMode);
        }

        rigidBody.velocity = moveDirection;
    }

    void UpdateAnimation() {

        if ( jumping || !collision.isGrounded ) {
            return;
        }

        if (moveDirection == Vector3.zero) {
            controller.animation.OnIdle();
        } else if (speed == walkSpeed) {
            controller.animation.OnWalk();
        } else if (speed == runSpeed){
            controller.animation.OnSprint();
        }
    }

    void UpdateRotation() {
        Vector3 rotation = controller.input.inputHorizontal * rightAxis + controller.input.inputVertical * forwardAxis;

        if ( rotation != Vector3.zero ) {
            transform.rotation = Quaternion.LookRotation(rotation);
        }
    }

    IEnumerator Jump() {
        rigidBody.velocity = Vector3.zero;
        float timer = 0.1f;

        while(controller.input.jumpPressed && timer < jumpTime) {
            if ( timer > jumpTime / 2 ) {
                jumping = false;
            }

            float proportionCompleted = timer / jumpTime;
            Vector3 currentJumpVector = Vector3.Lerp(jumpVector, Vector3.zero, proportionCompleted);
            currentJumpVector += moveDirection * jumpForwardMultiplier;

            rigidBody.AddForce(currentJumpVector);

            timer += Time.deltaTime;
            yield return null;
        }
    }
}
