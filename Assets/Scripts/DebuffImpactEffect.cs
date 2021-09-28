using UnityEngine;

public class DebuffImpactEffect : MonoBehaviour, IImpactEffect
{
    public StatusEffectFactory statusEffectFactory;

    public void ApplyEffect(GameObject target)
    {
        var statusEffectHandler = target.GetComponent<StatusEffectsHandler>();
        if(statusEffectHandler  != null)
        {
            statusEffectHandler.AddEffect(statusEffectFactory);
        }
    }
}
