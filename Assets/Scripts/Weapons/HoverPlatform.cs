using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoverPlatform : MonoBehaviour
{
    public bool activated = false;

    [Space(10)]
    public Material regularMaterial;
    public Material activatedMaterial;
    public Material disabledMaterial;
    private void OnCollisionEnter(Collision collision)
    {
        activated = true;
        ChangeMaterialTo(activatedMaterial);
        StartCoroutine("DisableAfterSeconds", 2);
    }

    void DisableMovement()
    {
        GetComponent<Rigidbody>().isKinematic = true;
    }
    private void ChangeMaterialTo(Material material)
    {
        GetComponent<Renderer>().material = material;
    }

    private IEnumerator DisableAfterSeconds(float seconds = 3) 
    {
        yield return new WaitForSeconds(seconds);

        DisableMovement();
        ChangeMaterialTo(disabledMaterial);
    }

}
