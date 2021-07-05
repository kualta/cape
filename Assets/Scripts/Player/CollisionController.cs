using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionController : MonoBehaviour
{
    [SerializeField]
    internal PlayerController controller;

    [SerializeField]
    internal Collider characterCollider;

    public bool stairForward = false;
    public LayerMask layerMask;
    public bool isGrounded;
    public bool wasGrounded;

    void FixedUpdate() {
        CheckForGround();
        CheckForStairs();
    }

    void OnCollisionEnter(Collision collision) {
        if (collision.gameObject.tag != "Enemy") {
            return;
        }
        ContactPoint contact = collision.contacts[0];
        Vector3 position = contact.point;
        Quaternion rotation = Quaternion.FromToRotation(Vector3.up, contact.normal);

        float amount = Random.Range(1, 34);
        controller.ReduceHealth(amount);
    }
    void CheckForStairs() {
        Vector3 rayOrigin = transform.position + controller.movement.moveDirection.normalized * 0.6f + Vector3.up * 0.8f;
        Vector3 rayDirection = Vector3.down;
        float rayLenght = 0.6f;

        if (controller.movement.moveDirection != Vector3.zero) {
            stairForward = Physics.Raycast(rayOrigin, rayDirection, rayLenght, layerMask);
        } else {
            stairForward = false;
        }

        if ( stairForward ) {
            Debug.DrawRay(rayOrigin, rayDirection * rayLenght, Color.red);
        }
    }
    void CheckForGround() {
        float distanceToGround = characterCollider.bounds.extents.y;

        Debug.DrawRay(transform.position + Vector3.up, Vector3.down * (distanceToGround + 0.01f));

        wasGrounded = isGrounded;
        isGrounded = Physics.Raycast(transform.position + Vector3.up, Vector3.down, distanceToGround + 0.01f);
    }
}
