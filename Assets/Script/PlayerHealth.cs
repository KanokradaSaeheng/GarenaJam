using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    public float maxHealth = 100f;
    public float currentHealth;

 

    private void Start()
    {
        currentHealth = maxHealth;
    
    }

    public void TakeDamage(float damage)
    {
        currentHealth -= damage;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);
        

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    

    private void Die()
    {
        Debug.Log("Player Died 💀");
        // Add death animation or reload scene here
    }

    // 👇 Just for testing
    public void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            Debug.Log("take 20 damage");
            TakeDamage(20f);
        }
    }

}