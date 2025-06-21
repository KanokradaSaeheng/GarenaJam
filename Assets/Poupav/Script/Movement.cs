using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f;
    public SimpleJoysticks joystick; // Assign in Inspector
    private Rigidbody rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        Vector3 move = new Vector3(joystick.Horizontal, 0f, joystick.Vertical);
        rb.MovePosition(rb.position + move * moveSpeed * Time.fixedDeltaTime);
    }
}
