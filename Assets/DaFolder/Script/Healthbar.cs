using UnityEngine;
using UnityEngine.UI;

public class Healthbar : MonoBehaviour
{
    public Transform cam;
    public Slider healthSlider;
    public float maxHealth = 100f;
    private float currentHealth;

    public GameObject deathOverlay; // Drag the DeathOverlay UI object here

    void Start()
    {
        currentHealth = maxHealth;
        UpdateHealthUI();

        if (deathOverlay != null)
            deathOverlay.SetActive(false); // Hide at start
    }

    void LateUpdate()
    {
        if (cam != null)
            transform.LookAt(transform.position + cam.forward);
    }

    public void TakeDamage(float damage)
    {
        currentHealth -= damage;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);
        UpdateHealthUI();

        if (currentHealth <= 0)
        {
            ShowDeathOverlay();
        }
    }

    void ShowDeathOverlay()
    {
        Debug.Log("Player Died 💀");
        if (deathOverlay != null)
            deathOverlay.SetActive(true);
    }

    void UpdateHealthUI()
    {
        if (healthSlider != null)
            healthSlider.value = currentHealth / maxHealth;
    }
}
