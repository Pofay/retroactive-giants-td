using System;
using System.Collections;
using UnityEngine;

public class EnemyParticleEmissionReceiver : MonoBehaviour
{
    private EnemyHealth health;
    private bool isReadyToReceiveDamage;
    private bool isCoroutineStarted;

    void Start()
    {
        health = GetComponent<EnemyHealth>();
        isReadyToReceiveDamage = true;

    }

    void OnParticleCollision(GameObject other)
    {
        var turret = other.GetComponentInParent<ParticleEmittingTurret>();
        if (isReadyToReceiveDamage)
        {
            health.TakeDamage(turret.damage);
            isReadyToReceiveDamage = false;
        }
        else
        {
            if (!isCoroutineStarted && gameObject.activeSelf)
            {
                StartCoroutine(RunNextDamageInterval(turret.damageInterval));
            }
        }
    }

    private IEnumerator RunNextDamageInterval(float damageInterval)
    {
        isCoroutineStarted = true;
        yield return new WaitForSeconds(damageInterval);
        isReadyToReceiveDamage = true;
        isCoroutineStarted = false;
        Debug.Log("Was Called");
        yield return null;
    }
}
