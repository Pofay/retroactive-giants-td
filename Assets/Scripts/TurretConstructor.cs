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
        var turretPosition = node.transform.position + offset;
        var asyncOperationHandle = selectedTurretToBuild.turretReference.InstantiateAsync(turretPosition, transform.rotation);
        asyncOperationHandle.Completed += (handle) =>
        {
            SpawnBuildParticles(turretPosition);
            var turretGO = handle.Result;
            node.mountedTurretGO = turretGO;
            playerStats.ReduceCurrency(selectedTurretToBuild.cost);
            turrets.Add(turretGO);
        };
    }

    public void BuildUpgradedTurret(Node node, Vector3 offset)
    {
        var mountedTurret = node.mountedTurretGO.GetComponent<Turret>();
        var turretPosition = node.transform.position + offset;
        SpawnBuildParticles(turretPosition);
        var turretGO = Instantiate(mountedTurret.upgradedVersion, turretPosition, transform.rotation);
        node.mountedTurretGO = turretGO;
        playerStats.ReduceCurrency(mountedTurret.upgradeCost);
        Destroy(mountedTurret.gameObject);
    }

    private void SpawnBuildParticles(Vector3 turretPosition)
    {
        var buildParticles = Instantiate(buildEffect, turretPosition, Quaternion.identity);
        Destroy(buildParticles, buildParticles.GetComponentInChildren<ParticleSystem>().main.duration);
    }

    public void RefundTurret(Turret t)
    {
        playerStats.AddCurrency(t.cost);
        Destroy(t.gameObject);
    }
}
