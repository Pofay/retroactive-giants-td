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
        var buildableTurrets = turretConstructor.buildableTurrets;
        for (var i = 0; i < buildableTurrets.Length; i++)
        {
            shopButtons[i].OnHoverText = buildableTurrets[i].turretName;
            textComponents[i].text = string.Format(" $ {0}", buildableTurrets[i].cost.ToString());
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
