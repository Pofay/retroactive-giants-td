using UnityEngine;

[CreateAssetMenu(fileName = "Damage Modifier", menuName = "Damage Modifiers/Increase against status effect")]
public class DamageModifier : ScriptableObject
{
    [SerializeField]
    public string EffectId;
    [Range(0f, 0.30f)]
    public float modifierPercentage;

    public float CalculateDamage(float damage, GameObject target)
    {
        var statusEffectHandler = target.GetComponent<StatusEffectsHandler>();
        if (statusEffectHandler != null)
        {
            if (statusEffectHandler.ContainsActiveEffect(EffectId))
            {
                return (damage * modifierPercentage);
            }
        }
        return 0;
    }
}
