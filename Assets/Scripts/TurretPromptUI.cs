using UnityEngine;
using UnityEngine.UI;

public class TurretPromptUI : MonoBehaviour
{
    [SerializeField] private Transform canvas;
    [SerializeField] private Button sellButton;

    public void Show()
    {
        canvas.gameObject.SetActive(true);
    }

    public void Hide()
    {
        canvas.gameObject.SetActive(false);
    }

    public void TransferPosition(Vector3 targetPosition)
    {
        this.gameObject.transform.position = targetPosition;
    }

    public void AttachButtonEvents(Node n)
    {
        sellButton.onClick.AddListener(() => n.SellTurret());
    }

    public void DetachButtonEvents()
    {
        sellButton.onClick.RemoveAllListeners();
    }
}
