using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    public float maxHealth = 100f;
    public float currentHealth;

    public HealthBar healthBar;
    public GameObject deathOverlay; // ⬅️ Assign in Inspector

    private void Start()
    {
        currentHealth = maxHealth;
        healthBar.SetMaxHealth((int)maxHealth);
        healthBar.SetHealth((int)currentHealth);
        if (deathOverlay != null)
            deathOverlay.SetActive(false); // Start hidden
    }

    public void TakeDamage(float damage)
    {
        currentHealth -= damage;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);
        healthBar.SetHealth((int)currentHealth);

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        Debug.Log("Player Died 💀");
        if (deathOverlay != null)
        {
            deathOverlay.SetActive(true); // Show death screen
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            Debug.Log("take 20 damage");
            TakeDamage(20f);
        }
    }

}