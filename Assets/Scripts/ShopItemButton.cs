using System;
using UnityEngine;
using UnityEngine.UI;

public class ShopItemButton : MonoBehaviour
{
    private Toggle toggle;
    private Animator animator;

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
