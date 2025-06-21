using UnityEngine;

public class PlayerMovementAnim : MonoBehaviour
{
    public float moveSpeed = 5f;
    public SimpleJoysticks joystick; // Assign in Inspector

    private Rigidbody rb;
    private Animator animator;
    private bool isFalling = false;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        animator = GetComponentInChildren<Animator>(); // Animator should be on the sprite
    }

    private void FixedUpdate()
    {
        if (isFalling) return; // Stop movement during fall

        Vector3 move = new Vector3(joystick.Horizontal, 0f, joystick.Vertical);
        rb.MovePosition(rb.position + move * moveSpeed * Time.fixedDeltaTime);

        // Update animation speed parameter
        float speed = move.magnitude;
        animator.SetFloat("Speed", speed);
    }

    // Call this to trigger fall animation
    public void TriggerFall()
    {
        if (isFalling) return;
        isFalling = true;
        animator.SetTrigger("Fall");
        Invoke(nameof(RecoverFromFall), 1.5f); // Adjust based on your fall animation length
    }

    private void RecoverFromFall()
    {
        isFalling = false;
    }
}
