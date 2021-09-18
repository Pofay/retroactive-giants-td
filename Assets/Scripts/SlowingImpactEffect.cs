using UnityEngine;

public class SlowingImpactEffect : MonoBehaviour, IImpactEffect
{
    [Header("Slowing Effect settings")]
    public float slowDuration = 3f;
    [Range(0f, 1f)] public float slowPercentage = 0.3f;

    public void ApplyEffect(Transform target)
    {
        var slowStatusEffect = new CryoSlowEffect(target.gameObject, slowPercentage, slowDuration);
        var statusEffectHandler = target.GetComponent<StatusEffectsHandler>();
        if(statusEffectHandler  != null)
        {
            statusEffectHandler.AddEffect(slowStatusEffect);
        }
    }
}
