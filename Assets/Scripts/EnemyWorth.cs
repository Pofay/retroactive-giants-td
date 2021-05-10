using UnityEngine;

public class EnemyWorth : MonoBehaviour
{
    public int currencyOnKill = 25;
    public int livesOnKill = 0;

    void OnDestroy()
    {
        FindObjectOfType<PlayerStats>().AddCurrency(currencyOnKill);
    }

}
