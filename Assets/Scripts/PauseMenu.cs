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

    void Awake()
    {
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
                pauseScreen.SetActive(true);
                Time.timeScale = 0f;
            }
            else
            {
                Unpause();
            }
        }
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
        SceneManager.LoadScene("MainMenu");
    }
}
