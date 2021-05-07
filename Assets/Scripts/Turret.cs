using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Screams to be called TurretTargeting
public abstract class Turret : MonoBehaviour
{

    [Header("Currency cost")]
    public int cost;

    [Header("General Combat Attributes")]
    public float range = 15f;

    protected TurretTargeting targeting;
    protected Transform target;

    void Start()
    {
        InvokeRepeating("UpdateTarget", 0, 0.5f);
        targeting = GetComponent<TurretTargeting>();
    }

    protected virtual void UpdateTarget()
    {
        target = targeting.CalculateNextTarget(range);
    }

    protected abstract void Update();

    protected abstract void Shoot();

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, range);
    }
}
