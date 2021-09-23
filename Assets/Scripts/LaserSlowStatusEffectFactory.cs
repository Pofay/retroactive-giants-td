using UnityEngine;

[CreateAssetMenu(fileName = "Status Effects", menuName = "Status Effects/Laser Slow")]
public class LaserSlowStatusEffectFactory : StatusEffectFactory
{
    public override string EffectId => "LASER_SLOW_EFFECT";

    [Header("Laser Slow Effect Values")]
    [Range(0f, 1f)]
    public float slowPercentage;
    [Range(1f, 5f)]
    public float duration;

    public override IStatusEffect CreateEffect()
    {
        return new LaserSlowEffect(slowPercentage, duration);
    }
}
