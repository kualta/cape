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
        if ( !activated )
        {
            activated = true;
            ChangeMaterialTo(activatedMaterial);
            StartCoroutine("FreezeAfterSeconds", 1.2f);
        }
    }
    private IEnumerator FreezeAfterSeconds(float seconds) 
    {
        yield return new WaitForSeconds(seconds);

        FreezeMovement();
        ChangeMaterialTo(disabledMaterial);
    }
    void FreezeMovement()
    {
        GetComponent<Rigidbody>().isKinematic = true;
    }
    private void ChangeMaterialTo(Material material)
    {
        GetComponent<Renderer>().material = material;
    }
}
