using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LivesRemover : MonoBehaviour
{
    private PlayerStats playerStats;
    private AudioSource audioSource;

    void Awake()
    {
        playerStats = FindObjectOfType<PlayerStats>();
        audioSource = GetComponent<AudioSource>();
    }

    void OnTriggerEnter(Collider other)
    {
        RemoveLives(1);
        audioSource.Play();
        other.gameObject.SetActive(false);
    }

    public void RemoveLives(int amount)
    {
        playerStats.RemoveLives(amount);
    }
}
