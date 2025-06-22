using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f;
    public SimpleJoysticks joystick; // Assign in Inspector
    private Rigidbody rb;

    [Header("Footstep Sound")]
    public AudioClip footstepClip;
    public float stepRate = 0.5f;
    private AudioSource audioSource;
    private float stepTimer;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();
    }

    private void FixedUpdate()
    {
        Vector3 move = new Vector3(joystick.Horizontal, 0f, joystick.Vertical);
        rb.MovePosition(rb.position + move * moveSpeed * Time.fixedDeltaTime);

        PlayFootstepSound();
    }

    private void PlayFootstepSound()
    {
        float speed = rb.velocity.magnitude;

        if (speed > 0.1f)
        {
            stepTimer -= Time.fixedDeltaTime;

            if (stepTimer <= 0f && footstepClip != null)
            {
                audioSource.PlayOneShot(footstepClip);
                stepTimer = stepRate;
            }
        }
        else
        {
            stepTimer = 0f;
        }
    }
}
