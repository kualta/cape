using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionController : MonoBehaviour
{
    [SerializeField]
    internal PlayerController controller;

    [SerializeField]
    internal Collider characterCollider;

    public bool isGrounded;

    void Update() {
        CheckForGround();
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

    void CheckForGround() {
        float distanceToGround = characterCollider.bounds.extents.y;

        Debug.DrawRay(transform.position + Vector3.up, Vector3.down * (distanceToGround + 0.1f));

        isGrounded = Physics.Raycast(transform.position + Vector3.up, Vector3.down, distanceToGround + 0.1f);
    }
}
