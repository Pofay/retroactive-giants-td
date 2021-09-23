using UnityEngine;

[CreateAssetMenu(fileName = "Status Effects", menuName = "Status Effects/Flamethrower Burn")]
public class BurnEffectFactory : StatusEffectFactory
{
    [Header("Burn Effect Values")]
    [Range(3f, 9f)]
    public float damage;
    [Range(0f, 3f)]
    public float tickInterval;
    [Range(1f, 6f)]
    public float duration;

    public override IStatusEffect CreateEffect()
    {
        return new BurnEffect(effectId, damage, tickInterval, duration);
    }
}
