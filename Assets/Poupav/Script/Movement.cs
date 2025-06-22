using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f;
    public SimpleJoysticks joystick; // Assign in Inspector
    public GameObject smokeEffectPrefab; // Drag your smoke effect prefab here
    public Transform smokeSpawnPoint; // Optional: Where to spawn the effect (e.g. under the player)

    private Rigidbody rb;
    private float smokeCooldown = 0.1f; // Control how often the smoke spawns
    private float smokeTimer = 0f;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        Vector3 move = new Vector3(joystick.Horizontal, 0f, joystick.Vertical);

        if (move.magnitude > 0.1f)
        {
            rb.MovePosition(rb.position + move * moveSpeed * Time.fixedDeltaTime);

            // Spawn smoke effect with cooldown
            smokeTimer -= Time.fixedDeltaTime;
            if (smokeTimer <= 0f)
            {
                SpawnSmoke();
                smokeTimer = smokeCooldown;
            }
        }
    }

    private void SpawnSmoke()
    {
        if (smokeEffectPrefab != null)
        {
            Vector3 spawnPosition = smokeSpawnPoint != null ? smokeSpawnPoint.position : transform.position;
            Instantiate(smokeEffectPrefab, spawnPosition, Quaternion.identity);
        }
    }
}