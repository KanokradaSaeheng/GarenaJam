using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public int health = 100;
    public int currentHealth;

    public HealthBar healthbar;

    void start()
    {
        currentHealth = health;
        healthbar.SetMaxHealth(health);
    }

    void update()
    {
        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
        {
            TakeDamage(10);
        }
    }

    void TakeDamage(int damage)
    {
        currentHealth -= damage;
        
        healthbar.SetHealth(currentHealth);
    }
}