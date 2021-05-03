using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpawnCountdownIndicator : MonoBehaviour
{
    public Text countdownIndicator;
    private WaveSpawner spawner;

    private void Awake()
    {
        spawner = FindObjectOfType<WaveSpawner>();
    }

    public void Update()
    {
        countdownIndicator.text = Math.Round(spawner.countdown).ToString();
    }
}
