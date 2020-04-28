using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretConstructor : MonoBehaviour
{

    public GameObject standardTurretPrefab;
    private GameObject turretToBuild;

    public GameObject GetTurretToBuild()
    {
        return turretToBuild;
    }

    void Start()
    {
        turretToBuild = standardTurretPrefab; 
    }
}
