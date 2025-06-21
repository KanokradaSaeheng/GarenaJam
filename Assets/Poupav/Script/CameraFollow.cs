using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target;        // Assign your player here
    public Vector3 offset = new Vector3(0, 5, -7); // Height + behind the player
    public float smoothSpeed = 5f;  // Higher = snappier, lower = smoother

    private void LateUpdate()
    {
        if (target == null) return;

        Vector3 desiredPosition = target.position + offset;
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed * Time.deltaTime);

        transform.position = smoothedPosition;
        transform.LookAt(target); // Optional: Camera always looks at player
    }
}