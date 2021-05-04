using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerStats))]
public class TurretConstructor : MonoBehaviour
{
    [Header("Buildable Turrets")]
    public GameObject[] turretPrefabs;

    private PlayerStats playerStats;
    private GameObject turretToBuild;

    void Awake()
    {
        playerStats = GetComponent<PlayerStats>();
    }

    public bool CanBuildTurret()
    {
        if(turretToBuild != null)
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
        var turretGO = Instantiate(turretToBuild, turretPosition, transform.rotation);
        node.turret = turretGO;
        playerStats.ReduceCurrency(turret.cost);
    }
}
