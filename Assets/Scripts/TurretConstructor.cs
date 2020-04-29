using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretConstructor : MonoBehaviour
{

    public GameObject[] turretPrefabs;

    private GameObject turretToBuild;

    public GameObject GetTurretToBuild()
    {
        return turretToBuild;
    }

    public void SetTurretToConstruct(int turretIndex)
    {
        turretToBuild = turretPrefabs[turretIndex];
    }
}
