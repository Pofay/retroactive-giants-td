using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    public Button playButton;
    public Button quitButton;

    private SceneFader sceneFader;

    void Awake()
    {
        sceneFader = FindObjectOfType<SceneFader>();
        playButton.onClick.AddListener(() => Play());
        quitButton.onClick.AddListener(() => Quit());
    }

    public void Play()
    {
        sceneFader.FadeTo("LevelSelect");
    }

    public void Quit()
    {
        Application.Quit();
    }
}
