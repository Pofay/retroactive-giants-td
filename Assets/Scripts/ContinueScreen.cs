using UnityEngine;
using UnityEngine.UI;

public class ContinueScreen : MonoBehaviour
{
    public GameObject continueScreen;
    public Button toNextLevelButton;
    public Button toLevelSelectionButton;

    private LevelUnlocker unlocker;
    private SceneFader sceneFader;

    void Awake()
    {
        sceneFader = FindObjectOfType<SceneFader>();
        continueScreen.SetActive(false);
        unlocker = FindObjectOfType<LevelUnlocker>();
        unlocker.OnAllEnemiesDisabled += ShowWinScreen;
        toNextLevelButton.onClick.AddListener(() => ToNextLevel());
        toLevelSelectionButton.onClick.AddListener(() => ToLevelSelection());
    }

    void ToLevelSelection()
    {
        sceneFader.FadeTo("LevelSelect");
    }

    void ToNextLevel()
    {
        sceneFader.FadeTo(string.Format("Level{0}", unlocker.levelToUnlock));
    }

    void ShowWinScreen()
    {
        continueScreen.SetActive(true);
    }
}
