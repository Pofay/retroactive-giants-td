using UnityEngine;

public class SlowingImpactEffect : MonoBehaviour, IImpactEffect
{
    [Header("Slowing Effect settings")]
    public float slowDuration = 3f;

    [Range(0f, 1f)]
    public float slowPercentage = 0.3f;

    public StatusEffectFactory statusEffectFactory;

    public void ApplyEffect(Transform target)
    {
        var statusEffectHandler = target.GetComponent<StatusEffectsHandler>();
        if(statusEffectHandler  != null)
        {
            statusEffectHandler.AddEffect(statusEffectFactory);
        }
    }
}
