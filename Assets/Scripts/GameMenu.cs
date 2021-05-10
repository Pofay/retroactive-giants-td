using UnityEngine;

public class GameMenu : MonoBehaviour
{
    public GameObject gameOverScreen;

    private PlayerStats player;

    void Awake()
    {
        player = GetComponent<PlayerStats>();
        gameOverScreen.SetActive(false);
        player.OnLivesEmptied += TriggerGameOver;
    }

    void TriggerGameOver()
    {
        gameOverScreen.SetActive(true);
        Time.timeScale = 0f;
    }
}
