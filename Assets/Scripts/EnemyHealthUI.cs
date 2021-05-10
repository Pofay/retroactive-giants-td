using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHealthUI : MonoBehaviour
{
    [SerializeField] private Image healthSlider;

    private EnemyHealth health;

    void Start()
    {
        health = GetComponent<EnemyHealth>();
        health.OnHealthChanged += DisplayCurrentHealth;
    }

    private void DisplayCurrentHealth(float currentHealth, float maxHealth)
    {
        healthSlider.fillAmount = currentHealth / maxHealth;
    }

    void OnDestroy()
    {
        health.OnHealthChanged -= DisplayCurrentHealth;
    }
}
