using UnityEngine;

[CreateAssetMenu(fileName = "Status Effects", menuName = "Status Effects/Slow Effect")]
public class SlowStatusEffectFactory : StatusEffectFactory
{
    [Header("Slow Status Effect Values")]
    [Range(0f, 1f)]
    public float slowPercentage;
    [Range(1f, 5f)]
    public float duration;

    public override IStatusEffect CreateEffect()
    {
        return new SlowEffect(effectId, slowPercentage, duration);
    }
}
