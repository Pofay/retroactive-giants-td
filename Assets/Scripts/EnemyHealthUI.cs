using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHealthUI : MonoBehaviour
{
    [SerializeField] private Image healthSlider;
    private EnemyHealth health;
    private EnemyHealthUI healthUI;
    private Transform cam;

    void Awake()
    {
        health = GetComponentInParent<EnemyHealth>();
    }


    void Start()
    {
        cam = Camera.main.transform;
        health.OnDeath += HideUI;
        health.OnHealthChanged += DisplayCurrentHealth;
    }

    void LateUpdate()
    {
        var lookDirection = new Vector3(cam.position.x, transform.position.y, cam.position.z);
        transform.LookAt(lookDirection, Vector3.down);
    }

    private void HideUI()
    {
        gameObject.SetActive(false);
    }

    private void DisplayCurrentHealth(float currentHealth, float maxHealth)
    {
        healthSlider.fillAmount = currentHealth / maxHealth;
    }

    void OnDisable()
    {
        health.OnHealthChanged -= DisplayCurrentHealth;
        health.OnDeath -= HideUI;
    }

    void OnEnable()
    {
        health.OnHealthChanged += DisplayCurrentHealth;
        health.OnDeath += HideUI;
    }
}

