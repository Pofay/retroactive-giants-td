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
                Time.timeScale = 0f;
                pauseScreen.SetActive(true);
            }
            else
            {
                Unpause();
            }
        }
    }

    public void Retry()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void Unpause()
    {
        Time.timeScale = 1f;
        pauseScreen.SetActive(false);
    }

    void ToMainMenu()
    {
        Debug.Log("To Implement Main Menu Scene");
    }
}
