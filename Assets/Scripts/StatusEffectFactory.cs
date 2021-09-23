using UnityEngine;

public abstract class StatusEffectFactory : ScriptableObject
{
    public virtual string EffectId => "BASE_EFFECT";


    public abstract IStatusEffect CreateEffect();
}
