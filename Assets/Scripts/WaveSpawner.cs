using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveSpawner : MonoBehaviour
{
    public WaveDetails[] wavesForLevel;
    public float countdown = 5;
    [Min(0)] public int levelToUnlock;
    public event Action OnAllEnemiesDisabled;
    public event Action<int, int> OnWaveChanged;

    private LevelUnlocker levelUnlocker;
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
        levelUnlocker = new LevelUnlocker();
        currentWave = Instantiate(wavesForLevel[currentWaveIndex]);
        OnWaveChanged?.Invoke(currentWaveIndex +1 , wavesForLevel.Length);
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
                OnAllEnemiesDisabled?.Invoke();

                levelUnlocker.UnlockLevel(levelToUnlock);
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
            yield return new WaitForSeconds(0.5f);
        }
        currentRound++;
    }

    private bool IsCurrentWaveRoundFinished()
    {
        return currentRound == currentWave.numberOfRounds;
    }

    private void SpawnEnemy()
    {
        enemiesSpawned.Add(Instantiate(currentWave.enemyPrefab, transform.position, Quaternion.identity));
    }
}
