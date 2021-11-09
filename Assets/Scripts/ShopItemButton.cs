using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ShopItemButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    private Toggle toggle;
    private Animator animator;

    public string OnHoverText { get; internal set; }

    public void OnPointerEnter(PointerEventData eventData)
    {
        Tooltip.Show(OnHoverText);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        Tooltip.Hide();
    }

    void Awake()
    {
        toggle = GetComponent<Toggle>();
        animator = GetComponent<Animator>();
        toggle.onValueChanged.AddListener(TriggerAnimation);
        animator.SetBool("selected", false);
    }

    private void TriggerAnimation(bool isOn)
    {
        animator.SetBool("selected", isOn);
    }
}
