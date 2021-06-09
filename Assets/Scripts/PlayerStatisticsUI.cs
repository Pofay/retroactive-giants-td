using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerStatisticsUI : MonoBehaviour
{
    public TextMeshProUGUI currencyText;
    public TextMeshProUGUI livesText;

    [SerializeField] private PlayerStats stats;

    void Awake()
    {
        stats = GetComponent<PlayerStats>();
        stats.OnLivesChange += ShowLives;
        stats.OnCurrencyChange += ShowCurrency;
        stats.AddCurrency(0);
        stats.AddLives(0);
    }

    private void ShowCurrency(int currency)
    {
        currencyText.text = string.Format("{0}", currency);
    }

    private void ShowLives(int lives)
    {
        livesText.text = string.Format("{0}", lives);
    }

    void OnDestroy()
    {
        stats.OnCurrencyChange -= ShowCurrency;
        stats.OnLivesChange -= ShowLives;
    }
}
