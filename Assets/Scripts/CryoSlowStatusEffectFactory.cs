using UnityEngine;

[CreateAssetMenu(fileName = "Status Effects", menuName = "Status Effects/Cryo Freeze")]
public class CryoSlowStatusEffectFactory : StatusEffectFactory
{
    public override string EffectId => "CRYO_SLOW_EFFECT";

    [Header("Cryo Freeze Effect Values")]
    [Range(0f, 1f)]
    public float slowPercentage;
    [Range(1f, 5f)]
    public float duration;

    public override IStatusEffect CreateEffect()
    {
        return new CryoSlowEffect(slowPercentage, duration);
    }
}
