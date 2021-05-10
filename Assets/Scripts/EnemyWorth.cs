using UnityEngine;

public class EnemyWorth : MonoBehaviour
{
    public int currencyOnKill = 25;
    public int livesOnKill = 0;

    void OnDestroy()
    {
        var playerStats = FindObjectOfType<PlayerStats>();
        if(playerStats != null)
        {
            playerStats.AddCurrency(currencyOnKill);
            playerStats.AddLives(livesOnKill);
        }
    }

}
