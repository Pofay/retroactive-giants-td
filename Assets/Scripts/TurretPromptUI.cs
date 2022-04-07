using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TurretPromptUI : MonoBehaviour
{
    [SerializeField] private Transform canvas;
    [SerializeField] private Button sellButton;
    [SerializeField] private Button upgradeButton;

    [SerializeField] private TextMeshProUGUI upgradeValueText;
    [SerializeField] private TextMeshProUGUI sellValueText;

    void Awake()
    {
        Hide();
    }

    public void Show(Turret t)
    {
        canvas.gameObject.SetActive(true);
        sellValueText.text = t.cost.ToString();
        upgradeValueText.text = t.upgradeCost.ToString();
        if (t.IsUpgradeable)
        {
            upgradeButton.gameObject.SetActive(true);
            upgradeValueText.gameObject.SetActive(true);
        }
        else
        {
            upgradeButton.gameObject.SetActive(false);
            upgradeValueText.gameObject.SetActive(false);
        }
    }

    public void Hide()
    {
        canvas.gameObject.SetActive(false);
        sellValueText.text = "$";
        upgradeValueText.text = "$";
    }

    public void TransferPosition(Vector3 targetPosition)
    {
        this.gameObject.transform.position = targetPosition;
    }

    public void AttachButtonEvents(Node n)
    {
        sellButton.onClick.AddListener(() => n.SellTurret());
        upgradeButton.onClick.AddListener(() => n.UpgradeTurret());
    }

    public void DetachButtonEvents()
    {
        sellButton.onClick.RemoveAllListeners();
        upgradeButton.onClick.RemoveAllListeners();
    }
}
