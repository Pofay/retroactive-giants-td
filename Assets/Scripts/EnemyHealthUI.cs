using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHealthUI : MonoBehaviour
{
    [SerializeField] private Image healthSlider;

    private EnemyHealth health;

    void Awake()
    {
        health = GetComponent<EnemyHealth>();
    }

    void Start()
    {
        health.OnHealthChanged += DisplayCurrentHealth;
    }

    private void DisplayCurrentHealth(float currentHealth, float maxHealth)
    {
        healthSlider.fillAmount = currentHealth / maxHealth;
    }

    void OnDisable()
    {
        health.OnHealthChanged -= DisplayCurrentHealth;
    }

    void OnEnable()
    {
        health.OnHealthChanged += DisplayCurrentHealth;
    }
}

