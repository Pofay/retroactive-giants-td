using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LivesRemover : MonoBehaviour
{
    private PlayerStats playerStats;

    void Awake()
    {
        playerStats = FindObjectOfType<PlayerStats>();
    }

    public void RemoveLives(int amount)
    {
        playerStats.RemoveLives(amount);
    }
}
