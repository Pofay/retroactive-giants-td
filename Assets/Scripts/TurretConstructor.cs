using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;

[RequireComponent(typeof(PlayerStats))]
public class TurretConstructor : MonoBehaviour
{
    [Header("Buildable Turrets")]
    public BuildableTurretDefinition[] buildableTurrets;
    [Header("Build Effect")]
    public GameObject buildEffect;

    private PlayerStats playerStats;
    private BuildableTurretDefinition selectedTurretToBuild;

    private List<GameObject> turrets;

    void Awake()
    {
        playerStats = GetComponent<PlayerStats>();
        turrets = new List<GameObject>();
    }

    public bool CanBuildTurret()
    {
        if (selectedTurretToBuild != null)
        {
            return playerStats.HasEnoughCurrencyForCost(selectedTurretToBuild.cost);
        }
        return false;
    }

    public bool CanBuildUpgradedTurret(Turret t)
    {
        return playerStats.HasEnoughCurrencyForCost(t.upgradeCost);
    }

    public void SetTurretToConstruct(int turretIndex)
    {
        selectedTurretToBuild = buildableTurrets[turretIndex];
    }

    public void BuildTurret(Node node, Vector3 offset)
    {
        if (CanBuildTurret())
        {
            var turretPosition = node.transform.position + offset;
            InstantiateTurret(selectedTurretToBuild, node, turretPosition);
        }
    }

    public void BuildUpgradedTurret(Node node, Vector3 offset)
    {
        var turretPosition = node.transform.position + offset;
        var mountedTurret = node.mountedTurretGO.GetComponent<Turret>();
        InstantiateTurret(mountedTurret.upgradedVersion, node, turretPosition);
        DestroyTurret(node.mountedTurretGO);
    }
    public void RefundTurret(Turret t)
    {
        playerStats.AddCurrency(t.cost);
        DestroyTurret(t.gameObject);
    }

    private void InstantiateTurret(BuildableTurretDefinition turretSpecification, Node node, Vector3 turretPosition)
    {
        var asyncOperationHandle = turretSpecification.turretReference.InstantiateAsync(turretPosition, transform.rotation);
        asyncOperationHandle.Completed += (handle) =>
        {
            var turretGO = handle.Result;
            var turret = turretGO.GetComponent<Turret>();
            MountTurretOnNode(node, turretGO);
            turrets.Add(turretGO);
            SpawnBuildParticles(turretPosition);
            SetCostOfTurretFromDefinition(turretSpecification, turret);
            playerStats.ReduceCurrency(turretSpecification.cost);
        };
    }

    private void MountTurretOnNode(Node node, GameObject turretGO)
    {
        node.mountedTurretGO = turretGO;
    }

    private void SetCostOfTurretFromDefinition(BuildableTurretDefinition turretSpecification, Turret turret)
    {
        turret.cost = turretSpecification.cost;
        if (turretSpecification.HasUpgradedVariant())
        {
            turret.upgradeCost = turretSpecification.UpgradedTurretDefinition.cost;
            turret.upgradedVersion = turretSpecification.UpgradedTurretDefinition;
        }
    }

    private void SpawnBuildParticles(Vector3 turretPosition)
    {
        var buildParticles = Instantiate(buildEffect, turretPosition, Quaternion.identity);
        Destroy(buildParticles, buildParticles.GetComponentInChildren<ParticleSystem>().main.duration);
    }

    private void DestroyTurret(GameObject turret)
    {
        turrets.Remove(turret);
        Addressables.ReleaseInstance(turret);
    }
}
