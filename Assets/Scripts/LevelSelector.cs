using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelSelector : MonoBehaviour
{
    public GameObject selectionContent;
    public GameObject levelButtonPrefab;

    public int numberOfLevels = 3;


    void Awake()
    {
        int maxLevelUnlocked = PlayerPrefs.GetInt("maxLevelReached", 0);
        for (var i = 0; i < numberOfLevels; i++)
        {
            var levelButtonGO = Instantiate(levelButtonPrefab);
            levelButtonGO.transform.SetParent(selectionContent.transform);
            levelButtonGO.transform.localScale = new Vector3(1, 1, 1);
            var levelButton = levelButtonGO.GetComponent<Button>();
            var levelButtonText = levelButtonGO.GetComponentInChildren<TextMeshProUGUI>();
            levelButtonText.text = (i + 1).ToString();
            var levelIndex = i;
            levelButton.onClick.AddListener(() => LoadLevel(levelIndex));
            if(i > maxLevelUnlocked)
            {
                levelButton.interactable = false;
            }
        }
    }

    public void LoadLevel(int levelIndex)
    {
        SceneManager.LoadScene(levelIndex);
    }
}
