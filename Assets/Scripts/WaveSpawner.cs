using System;
using System.Collections;
using UnityEngine;

public class WaveSpawner : MonoBehaviour
{
    public WaveDetails[] wavesForLevel;
    public float countdown = 5;
    public bool isAllowedToSpawn = true;
    public GameObject spawnParent;

    public event Action<int, int> OnWaveChanged;
    public event Action<GameObject> OnSpawn;
    public bool IsFinishedSpawning { get; private set; }

    private WaveDetails currentWave;
    private int currentRound = 0;
    private int currentWaveIndex = 0;

    void Start()
    {
        IsFinishedSpawning = false;
        currentWave = Instantiate(wavesForLevel[currentWaveIndex]);
        OnWaveChanged?.Invoke(currentWaveIndex + 1, wavesForLevel.Length);
    }

    private void PrepareNextWave()
    {
        if (IsNextWaveAvailable())
        {
            currentRound = 0;
            currentWave = Instantiate(wavesForLevel[currentWaveIndex]);
            OnWaveChanged?.Invoke(currentWaveIndex + 1, wavesForLevel.Length);
        }
    }

    public void FixedUpdate()
    {
        if (IsNextWaveAvailable() && isAllowedToSpawn)
        {
            countdown -= Time.deltaTime;
            if (countdown < 0f && currentRound < currentWave.numberOfRounds)
            {
                StartCoroutine(BeginSpawning());
                ResetCountdown();
            }
        }
        else
        {
            IsFinishedSpawning = true;
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
        if (IsCurrentWaveRoundFinished())
        {
            currentWaveIndex++;
            PrepareNextWave();
        }
    }

    IEnumerator SpawnEnemiesForRound()
    {
        for (var i = 0; i < currentWave.enemiesPerRound; i++)
        {
            SpawnEnemy();
            yield return new WaitForSeconds(1.5f);
        }
        currentRound++;
    }

    private bool IsCurrentWaveRoundFinished()
    {
        return currentRound == currentWave.numberOfRounds;
    }

    private void SpawnEnemy()
    {
        //var enemy = Instantiate(currentWave.enemyPrefab, transform.position, Quaternion.identity);
        //OnSpawn?.Invoke(enemy);

        var asyncOperationHandle = currentWave.enemyAssetReference.InstantiateAsync(transform.position, Quaternion.identity, spawnParent.transform);
        asyncOperationHandle.Completed += (handle) =>
        {
            OnSpawn?.Invoke(handle.Result);
        };
    }
}
