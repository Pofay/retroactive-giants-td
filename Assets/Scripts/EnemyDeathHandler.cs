using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class EnemyDeathHandler : MonoBehaviour
{
    private Animator animator;

    private bool hasPerformedCleanup;

    void Start()
    {
        animator = GetComponentInChildren<Animator>();
        var health = GetComponent<EnemyHealth>();
        health.OnDeath += PerformCleanup;
        hasPerformedCleanup = false;
    }

    private void PerformCleanup()
    {
        if (!hasPerformedCleanup)
        {
            StartCoroutine(PerformDeathSequence());
        }
    }

    private IEnumerator PerformDeathSequence()
    {
        hasPerformedCleanup = true;
        animator.SetTrigger("Death");
        GetComponent<SphereCollider>().enabled = false;
        GetComponent<NavMeshAgent>().enabled = false;
        GetComponent<EnemyHealth>().enabled = false;
        GetComponentInChildren<EnemyHealthUI>().enabled = false;
        yield return new WaitForSeconds(2f);
        gameObject.SetActive(false);
        yield return new WaitForEndOfFrame();
    }
}
