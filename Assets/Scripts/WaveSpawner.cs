using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WaveSpawner : MonoBehaviour
{
    public Transform enemyPrefab;
    public float timeBetweenWaves = 5.5f;
    public float countdown = 2.5f;
    private int waveIndex = 0;

    public void Update()
    {
        if (countdown < 0f)
        {
            StartCoroutine(SpawnWave());
            ResetCountdown();
        }
        countdown -= Time.deltaTime;
    }

    void ResetCountdown()
    {
        countdown = timeBetweenWaves;
    }

    IEnumerator SpawnWave()
    {
        waveIndex++;
        for (var i = 0; i < waveIndex; i++)
        {
            Instantiate(enemyPrefab);
            yield return new WaitForSeconds(0.5f);
        }
    }



}
