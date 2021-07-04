using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField]
    internal PlayerController controller;

    public GameObject camera;
    public Vector3 cameraOffset;


    void Start() {
        if ( camera == null ) {
            Debug.LogError("Camera Controller::Start Error: No Camera Found!");
        }
    }

    void Update() {
        camera.transform.position = gameObject.transform.position + cameraOffset;
        camera.transform.rotation = Quaternion.LookRotation(cameraOffset * -1f, Vector3.up);
    }
}
