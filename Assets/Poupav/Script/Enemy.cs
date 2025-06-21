using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float maxHealth = 3f;
    private float currentHealth;

    public EnemySpawner spawner;

    [Header("Ally Conversion")]
    public GameObject allyPrefab; // 💡 assign in Inspector

    void Start()
    {
        currentHealth = maxHealth;
    }

    public void TakeDamage(float damage, GameObject attacker = null)
    {
        currentHealth -= damage;
        Debug.Log("Enemy took " + damage + " damage. HP left: " + currentHealth);

        if (currentHealth <= 0)
        {
            Die(attacker);
        }
    }

    private void Die(GameObject attacker)
    {
        // ✅ 1/4 chance to convert to ally if killed by an ally
        if (attacker != null && attacker.CompareTag("Ally"))
        {
            float roll = Random.Range(0f, 1f);
            if (roll <= 0.25f)
            {
                Debug.Log("Enemy converted into an Ally!");

                if (allyPrefab != null)
                {
                    GameObject newAlly = Instantiate(allyPrefab, transform.position, Quaternion.identity);

                    // Optional: assign target to follow player or anything
                    AllyUnit allyScript = newAlly.GetComponent<AllyUnit>();
                    AllyUnit attackerAlly = attacker.GetComponent<AllyUnit>();
                    if (allyScript != null && attackerAlly != null)
                    {
                        allyScript.playerTarget = attackerAlly.playerTarget;
                    }
                }
            }
        }

        if (spawner != null)
        {
            spawner.DecreaseEnemiesCount();
        }

        Destroy(gameObject);
    }

    private void OnDestroy()
    {
        if (spawner != null)
        {
            spawner.DecreaseEnemiesCount();
        }
    }
}