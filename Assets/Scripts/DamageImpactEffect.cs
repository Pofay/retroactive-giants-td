using System;
using UnityEngine;

public class DamageImpactEffect : MonoBehaviour, IImpactEffect
{
    [Header("Projectile Damage")]
    public float damage = 10;
    [Header("Damage Modifier")]
    public DamageModifier[] modifiers;

    public void ApplyEffect(GameObject target)
    {
        var enemyHealth = target.GetComponent<EnemyHealth>();
        if (enemyHealth != null)
        {
            CalculateDamageWithModifiers(damage, target.gameObject, enemyHealth);
        }
    }

    private void CalculateDamageWithModifiers(float damage, GameObject gameObject, EnemyHealth health)
    {
        var damageWithModifiers = damage;
        foreach (var modifier in modifiers)
        {
            damageWithModifiers += modifier.CalculateDamage(damage, gameObject);
        }
        health.TakeDamage(damageWithModifiers);
    }
}
