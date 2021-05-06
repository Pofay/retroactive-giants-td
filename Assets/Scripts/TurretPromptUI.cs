using UnityEngine;

public class TurretPromptUI : MonoBehaviour
{
    [SerializeField] private Transform canvas;

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
}
