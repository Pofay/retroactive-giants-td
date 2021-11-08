using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour
{
    public GameObject pauseScreen;
    public Button continueButton;
    public Button mainMenuButton;
    public Button retryButton;

    private SceneFader sceneFader;

    void Awake()
    {
        sceneFader = FindObjectOfType<SceneFader>();
        pauseScreen.SetActive(false);
        continueButton.onClick.AddListener(() => Unpause());
        mainMenuButton.onClick.AddListener(() => ToMainMenu());
        retryButton.onClick.AddListener(() => Retry());
    }

    public void Pause(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            if (!pauseScreen.activeSelf)
            {
                Pause();
            }
            else
            {
                Unpause();
            }
        }
    }

    private void Pause()
    {
        pauseScreen.SetActive(true);
        Time.timeScale = 0f;
    }

    public void Retry()
    {
        Unpause();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void Unpause()
    {
        Time.timeScale = 1f;
        pauseScreen.SetActive(false);
    }

    void ToMainMenu()
    {
        Unpause();
        sceneFader.FadeTo("MainMenu");
    }
}
