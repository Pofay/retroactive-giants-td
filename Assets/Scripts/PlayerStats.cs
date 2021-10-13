using System;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    public event Action<int> OnLivesChange;
    public event Action<int> OnCurrencyChange;
    public event Action OnLivesEmptied;

    [Header("Player Statistics")]
    [SerializeField] private int currency = 500;
    [SerializeField] private int lives = 20;

    public void ReduceCurrency(int amount)
    {
        if (currency > 0)
        {
            currency -= amount;
            OnCurrencyChange?.Invoke(currency);
        }
    }

    public void AddCurrency(int amount)
    {
        currency += amount;
        OnCurrencyChange?.Invoke(currency);
    }

    public void AddLives(int amount)
    {
        lives += amount;
        OnLivesChange?.Invoke(lives);
    }

    public void RemoveLives(int amount)
    {
        if (lives > 0)
        {
            lives -= amount;
            OnLivesChange?.Invoke(lives);
            if (lives <= 0)
            {
                OnLivesEmptied?.Invoke();
            }
        }
    }

    public bool HasEnoughCurrencyForTurret(Turret t)
    {
        return currency >= t.cost;
    }
}
