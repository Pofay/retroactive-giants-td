using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class EnemyDeathHandler : MonoBehaviour
{
    private Animator animator;
    private EnemyWorth enemyWorth;

    private bool hasStartedDeathSequence;

    void Start()
    {
        animator = GetComponentInChildren<Animator>();
        enemyWorth = GetComponent<EnemyWorth>();
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
        PerformDeathAnimation();
        DisableCollisionAndNavigation();
        HideHealthbar();
        enemyWorth.AddCurrencyToPlayer();
        yield return new WaitForSeconds(2f);
        gameObject.SetActive(false);
        yield return new WaitForEndOfFrame();
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
