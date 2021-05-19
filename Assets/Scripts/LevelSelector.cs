using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelSelector : MonoBehaviour
{
    public GameObject selectionContent;
    public GameObject levelButtonPrefab;
    public Button mainMenuButton;

    public int numberOfLevels = 3;
    private SceneFader sceneFader;


    void Awake()
    {
        sceneFader = FindObjectOfType<SceneFader>();
        int maxLevelUnlocked = PlayerPrefs.GetInt("maxLevelReached", 0);
        mainMenuButton.onClick.AddListener(() => ToMainMenu());
        for (var i = 0; i < numberOfLevels; i++)
        {
            GenerateLevelButton(maxLevelUnlocked, i);
        }
    }

    private void GenerateLevelButton(int maxLevelUnlocked, int i)
    {
        var levelButtonGO = Instantiate(levelButtonPrefab);
        levelButtonGO.transform.SetParent(selectionContent.transform);
        levelButtonGO.transform.localScale = new Vector3(1, 1, 1);
        var levelButton = levelButtonGO.GetComponent<Button>();
        var levelButtonText = levelButtonGO.GetComponentInChildren<TextMeshProUGUI>();
        levelButtonText.text = (i + 1).ToString();
        var sceneName = ("Level" + (i + 1));
        levelButton.onClick.AddListener(() => LoadLevel(sceneName));
        if (i > maxLevelUnlocked)
        {
            levelButton.interactable = false;
        }
    }

    public void ToMainMenu()
    {
        sceneFader.FadeTo("MainMenu");
    }

    public void LoadLevel(string level)
    {
        sceneFader.FadeTo(level);
    }
}
