using UnityEngine;

[RequireComponent(typeof(PlayerStats))]
public class TurretConstructor : MonoBehaviour
{
    [Header("Buildable Turrets")]
    public GameObject[] turretPrefabs;
    [Header("Build Effect")]
    public GameObject buildEffect;

    private PlayerStats playerStats;
    private GameObject turretToBuild;

    public GameObject[] AvailableTurrets => turretPrefabs;

    void Awake()
    {
        playerStats = GetComponent<PlayerStats>();
    }

    public bool CanBuildTurret()
    {
        if (turretToBuild != null)
        {
            var t = turretToBuild.GetComponent<Turret>();
            return playerStats.HasEnoughCurrencyForTurret(t);
        }
        return false;
    }

    public void SetTurretToConstruct(int turretIndex)
    {
        turretToBuild = turretPrefabs[turretIndex];
    }

    public void BuildTurret(Node node, Vector3 offset)
    {
        var turret = turretToBuild.GetComponent<Turret>();
        var turretPosition = node.transform.position + offset;
        SpawnBuildParticles(turretPosition);
        var turretGO = Instantiate(turretToBuild, turretPosition, transform.rotation);
        node.mountedTurretGO = turretGO;
        playerStats.ReduceCurrency(turret.cost);
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
