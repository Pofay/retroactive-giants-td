using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shop : MonoBehaviour
{
    private TurretConstructor turretConstructor;

    private void Awake()
    {
        turretConstructor = FindObjectOfType<TurretConstructor>();
    }
    public void PurchaseStandardTurret()
    {
        Debug.Log("Standard Turret Purchased");
        turretConstructor.SetTurretToConstruct(0);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
