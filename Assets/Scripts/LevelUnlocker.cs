using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelUnlocker : MonoBehaviour
{
    [SerializeField]
    [Range(1, 3)]
    public int levelToUnlock;

    public event Action OnAllEnemiesDisabled;
    public event Action OnLastLevelCompleted;

    [SerializeField]
    private int maxLevel = 3;
    [SerializeField]
    private bool isAtLastLevel = false;

    private WaveSpawner[] spawners;
    private List<GameObject> enemies;
    private bool isCheckingForWinCondition;

    void Start()
    {
        spawners = FindObjectsOfType<WaveSpawner>();
        InitializeSpawnCounter();
        isCheckingForWinCondition = false;
    }

    private void InitializeSpawnCounter()
    {
        enemies = new List<GameObject>();
        foreach (var spawner in spawners)
        {
            spawner.OnSpawn += AddToEnemyList;
        }
    }

    void OnDisable()
    {
        foreach (var spawner in spawners)
        {
            spawner.OnSpawn -= AddToEnemyList;
        }
    }

    private void AddToEnemyList(GameObject enemy)
    {
        enemies.Add(enemy);
    }

    void Update()
    {
        if (AllSpawnersHaveFinishedSpawning() && !isCheckingForWinCondition)
        {
            StartCoroutine(CheckIfPlayerHasWon());
        }
    }

    private bool AllSpawnersHaveFinishedSpawning()
    {
        var isFinishedSpawning = true;
        foreach (var spawner in spawners)
        {
            if (!spawner.IsFinishedSpawning)
            {
                isFinishedSpawning = false;
                return isFinishedSpawning;
            }
        }
        return isFinishedSpawning;
    }

    private IEnumerator CheckIfPlayerHasWon()
    {
        var frameInstruction = new WaitForEndOfFrame();
        isCheckingForWinCondition = true;
        yield return new WaitUntil(() => AreAllEnemiesDisabled());
        yield return frameInstruction;
        if (isAtLastLevel)
        {
            OnLastLevelCompleted?.Invoke();
            yield return frameInstruction;
        }
        else
        {
            OnAllEnemiesDisabled?.Invoke();
            UnlockLevel();
            yield return frameInstruction;
        }
    }

    private bool HasReachedMaxLevel(int currentUnlockedLevel)
    {
        return currentUnlockedLevel == maxLevel;
    }

    private bool AreAllEnemiesDisabled()
    {
        var allEnemiesDisabled = true;
        for (var i = 0; i < enemies.Count; i++)
        {
            if (enemies[i].activeSelf == true)
            {
                allEnemiesDisabled = false;
            }
        }
        return allEnemiesDisabled;
    }

    private void UnlockLevel()
    {
        var currentUnlockedLevels = PlayerPrefs.GetInt("maxLevelReached", 1);
        if (currentUnlockedLevels < levelToUnlock)
        {
            PlayerPrefs.SetInt("maxLevelReached", levelToUnlock);
        }
    }
}
