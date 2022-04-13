using System;
using UnityEngine;

public class DamageImpactEffect : MonoBehaviour, IImpactEffect
{
    [Header("Projectile Damage")]
    public float damage = 10;
    [Header("Damage Modifier")]
    public DamageModifier[] modifiers;

    [Header("Additional Modifiers")]
    public bool isAOE = false;
    [Range(1f, 8f)]
    public float explosiveRadius = 1f;


    private void OnDrawGizmosSelected()
    {
        if (isAOE)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, explosiveRadius);
        }
    }

    public void ApplyEffect(GameObject target)
    {
        if (isAOE)
        {
            ApplyDamageToAllInRadiusNearTarget(explosiveRadius);
        }
        else
        {
            ApplyDamageAndModifiers(target);
        }
    }

    private void ApplyDamageToAllInRadiusNearTarget(float explosiveRadius)
    {
        var colliders = Physics.OverlapSphere(transform.position, explosiveRadius);
        foreach (var collider in colliders)
        {
            if (collider.CompareTag("Enemy"))
            {
                ApplyDamageAndModifiers(collider.gameObject);
            }
        }
    }

    private void ApplyDamageAndModifiers(GameObject target)
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
