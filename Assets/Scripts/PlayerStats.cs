using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    [Header("Player Statistics")]
    [SerializeField] private int currency = 500;
    [SerializeField] private int lives = 20;

    public void ReduceCurrency(int amount)
    {
        if (currency > 0)
        {
            currency -= amount;
        }
    }

    public void AddCurrency(int amount)
    {
        currency += amount;
    }

    public void AddLives(int amount)
    {
        lives += amount;
    }

    public void RemoveLives(int amount)
    {
        if(lives > 0)
        {
            lives -= amount;
        }
    }

    public bool HasEnoughCurrencyForTurret(Turret t)
    {
        return currency >= t.cost;
    }

    void Update()
    {
        Debug.Log(string.Format("Currency: {0}", currency));
    }
}
