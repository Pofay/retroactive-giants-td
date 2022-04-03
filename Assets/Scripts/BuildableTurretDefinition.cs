using UnityEngine;
using UnityEngine.AddressableAssets;

[CreateAssetMenu(menuName = "Turrets/Turret Specification", order = 1)]
public class BuildableTurretDefinition : ScriptableObject
{
    public AssetReference turretReference;
    public string turretName;
    public int cost;

    [SerializeField] private BuildableTurretDefinition upgradedDefinition;

    public bool HasUpgradedVariant()
    {
        return upgradedDefinition != null;
    }

    public BuildableTurretDefinition UpgradedTurretDefinition
    {
        get
        {
            return upgradedDefinition;
        }
    }
}
