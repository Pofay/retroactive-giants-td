using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameFinishedMenu : MonoBehaviour
{
    public GameObject gameFinishedScreen;
    public Button mainMenuButton;
    public Button quitButton;

    private SceneFader sceneFader;

    void Awake()
    {
        sceneFader = FindObjectOfType<SceneFader>();
        gameFinishedScreen.SetActive(false);
        var unlocker = FindObjectOfType<LevelUnlocker>();
        unlocker.OnLastLevelCompleted += ShowGameFinishedScreen;
        mainMenuButton.onClick.AddListener(() => ToMainMenu());
        quitButton.onClick.AddListener(() => Quit());
    }

    private void ShowGameFinishedScreen()
    {
        gameFinishedScreen.SetActive(true);
    }

    public void Unpause()
    {
        Time.timeScale = 1f;
        gameFinishedScreen.SetActive(false);
    }

    public void Quit()
    {
        Application.Quit();
    }

    void ToMainMenu()
    {
        Unpause();
        sceneFader.FadeTo("MainMenu");
    }
}
