using UnityEngine;

[CreateAssetMenu(fileName = "Status Effects", menuName = "Status Effects/Flamethrower Burn")]
public class BurnEffectFactory : StatusEffectFactory
{
    public override string EffectId => "FLAMETHROWER_EFFECT";

    [Header("Flamethrower Burn Effect Values")]
    [Range(3f, 9f)]
    public float damage;
    [Range(0f, 3f)]
    public float tickInterval;
    [Range(1f, 6f)]
    public float duration;

    public override IStatusEffect CreateEffect()
    {
        return new BurnEffect(damage, tickInterval, duration);
    }
}
