using Unity.VisualScripting;
using UnityEngine;

public class FaceCamera : MonoBehaviour
{
    private Camera observingCamera;

    void Start()
    {
        observingCamera = Camera.main;

        if(observingCamera.IsUnityNull())
            enabled= false;
    }

    void Update()
    {
        transform.LookAt(transform.position + observingCamera.transform.rotation * Vector3.forward, 
            observingCamera.transform.rotation * Vector3.up);
    }
}
