using System.Collections;
using UnityEngine;

public class WaveSpawner : MonoBehaviour
{
    public WaveDetails[] wavesForLevel;
    public float countdown = 5;

    private WaveDetails currentWave;
    private int currentRound = 0;
    private int currentWaveIndex = 0;

    void Start()
    {
        currentWave = Instantiate(wavesForLevel[currentWaveIndex]);
    }

    private void PrepareNextWave()
    {
        if (IsNextWaveAvailable())
        {
            currentWaveIndex++;
            currentRound = 0;
            currentWave = Instantiate(wavesForLevel[currentWaveIndex]);
        }
    }

    public void Update()
    {
        if (IsNextWaveAvailable())
        {
            if (countdown < 0f && currentRound < currentWave.numberOfRounds)
            {
                StartCoroutine(BeginSpawning());
                ResetCountdown();
            }
            countdown -= Time.deltaTime;
        }
    }

    private bool IsNextWaveAvailable()
    {
        return currentWaveIndex < wavesForLevel.Length;
    }

    void ResetCountdown()
    {
        countdown = currentWave.timeBetweenSpawns;
    }

    IEnumerator BeginSpawning()
    {
        yield return SpawnEnemiesForRound();
        if (IsCurrentWaveFinished())
        {
            PrepareNextWave();
        }
    }

    IEnumerator SpawnEnemiesForRound()
    {
        for (var i = 0; i < currentWave.enemiesPerRound; i++)
        {
            SpawnEnemy();
            yield return new WaitForSeconds(0.5f);
        }
        currentRound++;
    }

    private bool IsCurrentWaveFinished()
    {
        return currentRound == currentWave.numberOfRounds;
    }

    private void SpawnEnemy()
    {
        Instantiate(currentWave.enemyPrefab);
    }
}
