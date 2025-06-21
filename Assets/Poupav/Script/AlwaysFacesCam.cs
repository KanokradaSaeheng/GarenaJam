using UnityEngine;

public class AlwaysFacesCam : MonoBehaviour
{
    private Camera mainCam;

    void Start()
    {
        mainCam = Camera.main;
    }

    void LateUpdate()
    {
        if (mainCam != null)
        {
            // Make this object face the camera
            transform.LookAt(transform.position + mainCam.transform.rotation * Vector3.forward,
                mainCam.transform.rotation * Vector3.up);
        }
    }
}
