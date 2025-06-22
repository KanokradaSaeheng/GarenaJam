using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using UnityEngine.UI;
    

public class HealthBar : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public Slider Slider;
    public Gradient Gradient;
    public Image fill;
    public PlayerHealth currentHealth;
    public PlayerHealth MaxHealth;

    public void SetMaxHealth(int MaxHealth)
    {
        Slider.maxValue = MaxHealth;
        Slider.value = MaxHealth;

        fill.color = Gradient.Evaluate(1f);

    }

    public void SetHealth(int currentHealth)
    {
        Slider.value = currentHealth;
        fill.color = Gradient.Evaluate(Slider.normalizedValue);
    }
}
