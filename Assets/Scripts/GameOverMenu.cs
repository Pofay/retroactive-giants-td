using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameOverMenu : MonoBehaviour
{
    public GameObject gameOverScreen;
    public Button retryButton;
    public Button mainMenuButton;

    private PlayerStats player;

    void Awake()
    {
        player = GetComponent<PlayerStats>();
        gameOverScreen.SetActive(false);
        player.OnLivesEmptied += TriggerGameOver;
        retryButton.onClick.AddListener(() => Retry());
        mainMenuButton.onClick.AddListener(() => ToMenu());
    }

    void TriggerGameOver()
    {
        gameOverScreen.SetActive(true);
        Time.timeScale = 0f;
    }

    void Retry()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    void ToMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("MainMenu");
    }
}
