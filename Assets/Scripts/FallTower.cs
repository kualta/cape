using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallTower : MonoBehaviour
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
        for (int i = 1; i <= 13; ++i) {
            x++;
            transform.Rotate(i, 0, 0);
            yield return new WaitForSeconds(0.01f);
        }

        rigidBody.isKinematic = true;
    }
}
