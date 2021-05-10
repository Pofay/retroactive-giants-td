using System;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public event Action<float, float> OnHealthChanged;

    [SerializeField] private float maxHealth;

    private float currentHealth;

    void Awake()
    {
        currentHealth = maxHealth;
    }

    public void TakeDamage(float damage)
    {
        currentHealth -= damage;
        OnHealthChanged?.Invoke(currentHealth, maxHealth);
        if(currentHealth < 0)
        {
            Destroy(this.gameObject);
        }
    }
}
