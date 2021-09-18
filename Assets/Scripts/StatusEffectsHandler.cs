using System.Collections.Generic;
using UnityEngine;

public class StatusEffectsHandler : MonoBehaviour
{
    private IDictionary<string, IStatusEffect> effects;

    void Start()
    {
        effects = new Dictionary<string, IStatusEffect>();
    }

    public void AddEffect(IStatusEffect effect)
    {
        if (effects.ContainsKey(effect.Id))
        {
            var existingEffect = effects[effect.Id];
            if (existingEffect.IsActive)
            {
                existingEffect.RefreshDuration();
            }
            else
            {
                StartCoroutine(existingEffect.ApplyEffect(gameObject));
            }
        }
        else
        {
            StartCoroutine(effect.ApplyEffect(gameObject));
            effects[effect.Id] = effect;
        }
    }
}
