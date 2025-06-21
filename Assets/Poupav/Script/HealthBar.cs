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

    public void SetMaxHealth(int health)
    {
        Slider.maxValue = health;
        Slider.value = health;

        fill.color = Gradient.Evaluate(1f);

    }

    public void SetHealth(int health)
    {
        Slider.value = health;
        fill.color = Gradient.Evaluate(Slider.normalizedValue);
    }
}
