using UnityEngine;

public class DebuffImpactEffect : MonoBehaviour, IImpactEffect
{
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
