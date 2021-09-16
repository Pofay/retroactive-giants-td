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
        if(effects.ContainsKey(effect.Id))
        {
            effects[effect.Id].RefreshDuration();
        }
        else
        {
            effects[effect.Id] = effect;
        }
    }

    void FixedUpdate()
    {
        foreach(var entry in effects)
        {
            var effectId = entry.Key;
            var effect = entry.Value;
            if(effect.RunningDuration > 0)
            {
                effect.Tick(gameObject, Time.deltaTime);
            }
            else
            {
                effect.ResetStatus();
            }
        }
    }
}
