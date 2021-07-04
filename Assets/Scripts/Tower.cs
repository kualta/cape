using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour
{
    [SerializeField]
    private Rigidbody rigidBody;

    public Vector3 centerOfMass;
    public Vector3 addForceTo;

    void Start()
    {
        rigidBody = GetComponent<Rigidbody>();

        StartCoroutine(Fall());
    }

    IEnumerator Fall() {
        int x = 1;
        for (int i = 0; i < 12; i++) {
            x++;
            transform.Rotate(x, 0, 0);
            yield return new WaitForSeconds(0.01f);
        }

        rigidBody.isKinematic = true;
    }
}
