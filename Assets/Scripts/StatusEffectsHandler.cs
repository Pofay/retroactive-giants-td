using System.Collections.Generic;
using UnityEngine;

public class StatusEffectsHandler : MonoBehaviour
{
    private IDictionary<string, IStatusEffect> effects;

    void Start()
    {
        effects = new Dictionary<string, IStatusEffect>();
    }

    public void AddEffect(StatusEffectFactory effectFactory)
    {
        if (effects.ContainsKey(effectFactory.EffectId))
        {
            RerunOrRefreshExistingEffect(effectFactory);
        }
        else
        {
            CreateAndAddEffect(effectFactory);
        }
    }

    private void CreateAndAddEffect(StatusEffectFactory effectFactory)
    {
        var newEffect = effectFactory.CreateEffect();
        StartCoroutine(newEffect.ApplyEffect(gameObject));
        effects[effectFactory.EffectId] = newEffect;
    }

    private void RerunOrRefreshExistingEffect(StatusEffectFactory effectFactory)
    {
        var existingEffect = effects[effectFactory.EffectId];
        if (existingEffect.IsActive)
        {
            existingEffect.RefreshDuration();
        }
        else
        {
            StartCoroutine(existingEffect.ApplyEffect(gameObject));
        }
    }
}
