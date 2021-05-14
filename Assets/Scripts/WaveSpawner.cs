using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveSpawner : MonoBehaviour
{
    public WaveDetails[] wavesForLevel;
    public float countdown = 5;

    private IList<GameObject> enemiesSpawned;

    private WaveDetails currentWave;
    private int currentRound = 0;
    private int currentWaveIndex = 0;

    void Awake()
    {
        enemiesSpawned = new List<GameObject>();
    }

    void Start()
    {
        currentWave = Instantiate(wavesForLevel[currentWaveIndex]);
    }

    private void PrepareNextWave()
    {
        if (IsNextWaveAvailable())
        {
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
        else
        {
            if (AllEnemiesAreDisabled())
            {
                countdown = 0;
                Debug.Log("Should Win Level");
            }
        }
    }

    private bool AllEnemiesAreDisabled()
    {
        var allEnemiesDisabled = true;
        for (var i = 0; i < enemiesSpawned.Count; i++)
        {
            if (enemiesSpawned[i].activeSelf == true)
            {
                allEnemiesDisabled = false;
            }
        }
        return allEnemiesDisabled;
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
            currentWaveIndex++;
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
        enemiesSpawned.Add(Instantiate(currentWave.enemyPrefab));
    }
}
