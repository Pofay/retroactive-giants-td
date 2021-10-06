using System;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public event Action<float, float> OnHealthChanged;
    public event Action OnDeath;

    [SerializeField]
    private float maxHealth;
    private float currentHealth;

    void Start()
    {
        currentHealth = maxHealth;
    }

    public void TakeDamage(float damage)
    {
        if (currentHealth > 0)
        {
            currentHealth -= damage;
            OnHealthChanged?.Invoke(currentHealth, maxHealth);
        }
        else
        {
            OnDeath?.Invoke();
        }
    }
}
