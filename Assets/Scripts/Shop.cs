using TMPro;
using UnityEngine;

public class Shop : MonoBehaviour
{
    private TurretConstructor turretConstructor;

    void Awake()
    {
        turretConstructor = FindObjectOfType<TurretConstructor>();
    }

    void Start()
    {
        var textComponents = GetComponentsInChildren<TextMeshProUGUI>();
        var shopButtons = GetComponentsInChildren<ShopItemButton>();
        var turretsGO = turretConstructor.turretPrefabs;
        for (var i = 0; i < turretsGO.Length; i++)
        {
            var turret = turretsGO[i].GetComponent<Turret>();
            var shopButton = shopButtons[i].OnHoverText = turret.name;
            textComponents[i].text = string.Format(" $ {0}", turret.cost.ToString());
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

    public void PurchaseLaserBeamer()
    {
        turretConstructor.SetTurretToConstruct(2);
    }

    public void PurchaseFlamethrower()
    {
        turretConstructor.SetTurretToConstruct(3);
    }
}
