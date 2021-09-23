using UnityEngine;

public abstract class StatusEffectFactory : ScriptableObject
{
    public string effectId = "BASE_EFFECT";

    public abstract IStatusEffect CreateEffect();
}
