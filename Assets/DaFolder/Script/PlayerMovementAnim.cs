using UnityEngine;

public class PlayerMovementAnim : MonoBehaviour
{
    public float moveSpeed = 5f;
    public SimpleJoysticks joystick; // Assign in Inspector

    public GameObject smokeEffectPrefab; // 🔥 Drag smoke prefab here
    public Transform smokeSpawnPoint;     // Optional: place to spawn the smoke under player

    private Rigidbody rb;
    private Animator animator;
    private bool isFalling = false;

    private float smokeCooldown = 0.1f;
    private float smokeTimer = 0f;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
    }

    private void FixedUpdate()
    {
        if (isFalling) return;

        Vector3 move = new Vector3(joystick.Horizontal, 0f, joystick.Vertical);
        rb.MovePosition(rb.position + move * moveSpeed * Time.fixedDeltaTime);

        float speed = move.magnitude;
        animator.SetFloat("Speed", speed);

        // 🌫 Spawn smoke if moving
        if (speed > 0.1f)
        {
            smokeTimer -= Time.fixedDeltaTime;
            if (smokeTimer <= 0f)
            {
                SpawnSmoke();
                smokeTimer = smokeCooldown;
            }
        }
    }

    void SpawnSmoke()
    {
        if (smokeEffectPrefab != null)
        {
            Vector3 spawnPos = smokeSpawnPoint != null ? smokeSpawnPoint.position : transform.position;
            Instantiate(smokeEffectPrefab, spawnPos, Quaternion.identity);
        }
    }

    public void TriggerFall()
    {
        if (isFalling) return;
        isFalling = true;
        animator.SetTrigger("Fall");
        Invoke(nameof(RecoverFromFall), 1.5f);
    }

    private void RecoverFromFall()
    {
        isFalling = false;
    }
}