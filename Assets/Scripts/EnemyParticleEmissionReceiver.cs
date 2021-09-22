﻿using System;
using System.Collections;
using UnityEngine;

public class EnemyParticleEmissionReceiver : MonoBehaviour
{
    private EnemyHealth health;
    private bool isReadyToReceiveDamage;
    private bool isCoroutineStarted;
    private IStatusEffect burnEffect;
    private StatusEffectsHandler statusHandler;


    void Start()
    {
        health = GetComponent<EnemyHealth>();
        statusHandler = GetComponent<StatusEffectsHandler>();
        isReadyToReceiveDamage = true;
        burnEffect = new BurnEffect(6, 2, 6);
    }

    void OnParticleCollision(GameObject other)
    {
        var turret = other.GetComponentInParent<ParticleEmittingTurret>();
        if (isReadyToReceiveDamage)
        {
            health.TakeDamage(turret.damage);
            isReadyToReceiveDamage = false;
            statusHandler.AddEffect(burnEffect);
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
        yield return null;
    }
}
