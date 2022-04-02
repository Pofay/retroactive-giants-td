using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class Tooltip : MonoBehaviour
{
    private static Tooltip instance;

    [SerializeField] private TextMeshProUGUI tooltipText;
    [SerializeField] private RectTransform background;

    void Awake()
    {
        instance = this;
        if (background == null)
        {
            background = transform.Find("TooltipBackground").GetComponent<RectTransform>();
        }
        if (tooltipText == null)
        {
            tooltipText = transform.Find("TooltipText").GetComponent<TextMeshProUGUI>();
        }
        HideTooltip();
    }

    void Update()
    {
        Vector2 localPoint;
        var mousePosition = Mouse.current.position.ReadValue();
        RectTransformUtility.ScreenPointToLocalPointInRectangle(transform.parent.GetComponent<RectTransform>(), mousePosition, null, out localPoint);
        transform.localPosition = localPoint;
    }

    public void ShowTooltip(string text)
    {
        ActivateTooltip();

        tooltipText.SetText(text);
        var textPaddingSize = 4f;
        var backgroundSize = new Vector2(tooltipText.preferredWidth + textPaddingSize * 2f, tooltipText.preferredHeight + textPaddingSize * 2f);
        background.sizeDelta = backgroundSize;
    }

    private void ActivateTooltip()
    {
        background.gameObject.SetActive(true);
        tooltipText.gameObject.SetActive(true);
    }

    public void HideTooltip()
    {
        DisableTooltip();
    }

    private void DisableTooltip()
    {
        background.gameObject.SetActive(false);
        tooltipText.gameObject.SetActive(false);
    }

    public static void Show(string text)
    {
        instance.ShowTooltip(text);
    }

    public static void Hide()
    {
        instance.HideTooltip();
    }
}
