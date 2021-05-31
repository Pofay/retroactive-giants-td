using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class WaveIndicator : MonoBehaviour
{
    [SerializeField] private Image waveLevelSlider;
    [SerializeField] private TextMeshProUGUI waveLevelText;

    void Awake()
    {
        var spawner = FindObjectOfType<WaveSpawner>();
        spawner.OnWaveChanged += ShowWaveLevel;
    }

    private void ShowWaveLevel(int currentWaveLevel, int maxWaveLevel)
    {
        waveLevelText.text = string.Format("{0}/{1}", currentWaveLevel, maxWaveLevel);

        waveLevelSlider.fillAmount = (float) currentWaveLevel / maxWaveLevel;
    }
}
