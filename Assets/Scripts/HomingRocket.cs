using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HomingRocket : Rocket
{
    public Transform target;
    [Space(10)]

    public float minRotationSpeed;
    public float maxRotationSpeed;
    public float rotationSpeed;
    public float releaseDistance;

    [Space(10)]
    public bool released;

    public void Release() {
        released = true;
        speedMultiplier = 4f;
    }

    public void LockTarget(Transform newTarget) {
        released = false;
        speedMultiplier = 1f;

        if ( !target ) {
            target = newTarget;
        }
    }

    protected void SetParameters() {
        speed = Random.Range(minSpeed, maxSpeed);
        rotationSpeed = Random.Range(minRotationSpeed, maxRotationSpeed);
        SetRotation();
    }

    protected void SetTarget() {
        target = GameObject.Find("Player").transform;
    }

    void Start() {
        SetTarget();
        SetParameters();
        released = false;
        speedMultiplier = 1f;
    }

    void Update() {
        Rotate();
        Move();
        HandlePosition();
    }

    void Rotate() {
        if ( released ) {
            return;
        }

        //
        // FIXME: Hack on next line, needed for rockets not to touch floor (Player transform position is at it's feet)
        //                                      \/\/\/\/\/
        Vector3 relativePos = target.position + Vector3.up - transform.position;
        Quaternion expectedRotation = Quaternion.LookRotation(relativePos, Vector3.up);
        Quaternion currentRotation = Quaternion.Slerp(transform.rotation, expectedRotation, Time.deltaTime * rotationSpeed);

        transform.rotation = currentRotation;
    }


    void HandlePosition() {
        float distanceToTarget = CheckDistanceTo(target);

        if (distanceToTarget < releaseDistance && !released) {
            Release();
        } else if (distanceToTarget > releaseDistance * 3f && released) {
            LockTarget(target);
        }
    }
}
