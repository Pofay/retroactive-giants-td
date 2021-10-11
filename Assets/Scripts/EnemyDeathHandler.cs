using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class EnemyDeathHandler : MonoBehaviour
{
    private Animator animator;

    private bool hasStartedDeathSequence;

    void Start()
    {
        animator = GetComponentInChildren<Animator>();
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
        animator.SetTrigger("Death");
        GetComponent<SphereCollider>().enabled = false;
        GetComponent<NavMeshAgent>().enabled = false;
        GetComponent<EnemyHealth>().enabled = false;
        yield return new WaitForSeconds(2f);
        gameObject.SetActive(false);
        yield return new WaitForEndOfFrame();
    }
}
