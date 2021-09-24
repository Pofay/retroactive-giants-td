using UnityEngine;

public class DamageImpactEffect : MonoBehaviour, IImpactEffect
{
    [Header("Projectile Damage")]
    public float damage = 10;
    [Header("Damage Modifier")]
    public DamageModifier damageModifier;

    public void ApplyEffect(Transform target)
    {
        var enemyHealth = target.GetComponent<EnemyHealth>();
        if (enemyHealth != null)
        {
            var damageWithModifier = damageModifier.CalculateDamage(damage, target.gameObject);
            Debug.Log("Damage with Modifier: " + damageWithModifier);
            enemyHealth.TakeDamage(damageWithModifier);
        }
    }
}
