using UnityEngine;

public class billboard : MonoBehaviour
{
    private Camera cam;

    void Start()
    {
        cam = Camera.main; // Grab main camera
    }

    void LateUpdate()
    {
        if (cam != null)
        {
            // Match the forward direction of the camera (face it)
            transform.forward = cam.transform.forward;
        }
    }
}