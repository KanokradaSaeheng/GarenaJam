using UnityEngine;
using UnityEngine.UI;

public class Healthbar : MonoBehaviour
{
    public Transform cam; // Assign Main Camera here
    public Slider healthSlider; // Drag your UI slider here
    public float maxHealth = 100f;
    private float currentHealth;

    void Start()
    {
        currentHealth = maxHealth;
        UpdateHealthUI();
    }

    void LateUpdate()
    {
        // Make the health bar face the camera
        if (cam != null)
            transform.LookAt(transform.position + cam.forward);
    }

    void Update()
    {
        // TEST: Tap screen to reduce HP
        if (Input.GetMouseButtonDown(0))
        {
            TakeDamage(10f);
        }
    }

    public void TakeDamage(float damage)
    {
        currentHealth -= damage;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);
        UpdateHealthUI();
    }

    void UpdateHealthUI()
    {
        if (healthSlider != null)
            healthSlider.value = currentHealth / maxHealth;
    }
}
