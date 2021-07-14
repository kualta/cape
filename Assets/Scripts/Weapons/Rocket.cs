using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rocket : MonoBehaviour
{
    internal Vector3 targetPosition;

    [Space(10)]
    public float minSpeed;
    public float maxSpeed;
    public float speed;
    public float speedMultiplier;

    [Space(10)]
    public GameObject explosionPrefab;


    public float CheckDistanceTo(Transform target) {
        return Vector3.Distance(transform.position, target.position);
    }

    protected void Move() {
        transform.Translate(Vector3.forward * Time.deltaTime * speed * speedMultiplier, Space.Self);
    }

    protected void OnCollisionEnter(Collision collision) {
        ContactPoint contact = collision.contacts[0];
        Vector3 position = contact.point;
        Quaternion rotation = Quaternion.FromToRotation(Vector3.up, contact.normal);
        // Instantiate(explosionPrefab, position, rotation);
        Destroy(gameObject);
    }

    protected void SetParameters() {
        speed = Random.Range(minSpeed, maxSpeed);
        SetRotation();
    }

    protected void SetRotation() {
        Vector3 relativePos = targetPosition - transform.position;
        Quaternion rotation = Quaternion.LookRotation(relativePos, Vector3.up);
        transform.rotation = rotation;
    }

    protected void SetTarget() {
        GameObject player = GameObject.Find("Player");
        Vector3 nextMovement = player.GetComponent<MovementController>().moveDirection;
        targetPosition = player.transform.position + nextMovement;
    }

    void Start() {
        SetTarget();
        SetParameters();
    }

    void Update() {
        Move();
    }
}
