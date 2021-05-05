using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Shop : MonoBehaviour
{
    private TurretConstructor turretConstructor;

    private void Awake()
    {
        turretConstructor = FindObjectOfType<TurretConstructor>();
    }

    void Start()
    {
        var textComponents = GetComponentsInChildren<TextMeshProUGUI>();
        var turretsGO = turretConstructor.AvailableTurrets;
        for (var i = 0; i < turretsGO.Length; i++)
        {
            var turret = turretsGO[i].GetComponent<Turret>();
            textComponents[i].text = string.Format("$ {0}", turret.cost.ToString());
        }
    }

    public void PurchaseStandardTurret()
    {
        turretConstructor.SetTurretToConstruct(0);
    }

    public void PurchaseMissileLauncher()
    {
        turretConstructor.SetTurretToConstruct(1);
    }

}
