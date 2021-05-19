using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ContinueScreen : MonoBehaviour
{
    public GameObject continueScreen;
    public Button toNextLevelButton;
    public Button toLevelSelectionButton;

    private WaveSpawner spawner;
    private SceneFader sceneFader;

    void Awake()
    {
        sceneFader = FindObjectOfType<SceneFader>();
        continueScreen.SetActive(false);
        spawner = FindObjectOfType<WaveSpawner>();
        spawner.OnAllEnemiesDisabled += ShowWinScreen;
        toNextLevelButton.onClick.AddListener(() => ToNextLevel());
        toLevelSelectionButton.onClick.AddListener(() => ToLevelSelection());
    }

    void ToLevelSelection()
    {
        continueScreen.SetActive(false);
        sceneFader.FadeTo("LevelSelect");
    }

    void ToNextLevel()
    {
        sceneFader.FadeTo(string.Format("Level{0}", spawner.levelToUnlock + 1));
    }

    void ShowWinScreen()
    {
        continueScreen.SetActive(true);
    }


}
