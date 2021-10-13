using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class EnemyDeathHandler : MonoBehaviour
{
    private Animator animator;
    private EnemyWorth enemyWorth;
    private AudioSource audioSource;

    private bool hasStartedDeathSequence;

    void Start()
    {
        animator = GetComponentInChildren<Animator>();
        enemyWorth = GetComponent<EnemyWorth>();
        audioSource = GetComponent<AudioSource>();
        var health = GetComponent<EnemyHealth>();
        health.OnDeath += StartDeathSequence;
        hasStartedDeathSequence = false;
    }

    private void StartDeathSequence()
    {
        if (!hasStartedDeathSequence)
        {
            StartCoroutine(PerformDeathSequence());
        }
    }

    private IEnumerator PerformDeathSequence()
    {
        hasStartedDeathSequence = true;
        AddCurrencyToPlayer();
        PerformDeathAnimation();
        DisableCollisionAndNavigation();
        HideHealthbar();
        yield return new WaitForSeconds(2f);
        gameObject.SetActive(false);
        yield return new WaitForEndOfFrame();
    }

    private void AddCurrencyToPlayer()
    {
        audioSource.Play();
        enemyWorth.AddCurrencyToPlayer();
    }

    private void HideHealthbar()
    {
        GetComponent<EnemyHealth>().enabled = false;
    }

    private void DisableCollisionAndNavigation()
    {
        GetComponent<SphereCollider>().enabled = false;
        GetComponent<NavMeshAgent>().enabled = false;
    }

    private void PerformDeathAnimation()
    {
        animator.SetTrigger("Death");
    }
}
