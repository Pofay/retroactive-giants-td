using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Screams to be called TurretTargeting
public class Turret : MonoBehaviour
{

    [Header("Currency cost")]
    public int cost;

    [Header("General Combat Attributes")]
    public float range = 15f;

    [Header("Ballistic Turret Attribute(default)")]
    public float fireRate = 1f;
    private float fireCountdown = 0f;
    public GameObject bulletPrefab;

    [Header("Laser Turret Attribute")]
    public bool useLaser = false;
    public LineRenderer lineRenderer;
    public ParticleSystem impactEffect;
    public Light impactLight;
    public int damageOverTime = 30;
    [Range(0f, 1f)] public float slowPercentage = 0.2f;
    public float slowDuration = 2f;
    private EnemyMovement targetMovement;
    private EnemyHealth targetHealth;


    private Transform target;
    private TurretTargeting targeting;

    void Start()
    {
        InvokeRepeating("UpdateTarget", 0, 0.5f);
        targeting = GetComponent<TurretTargeting>();
    }

    void UpdateTarget()
    {
        target = targeting.CalculateNextTarget(range);
        if(useLaser && target != null)
        {
            targetHealth = target.GetComponent<EnemyHealth>();
            targetMovement = target.GetComponent<EnemyMovement>();
        }
    }

    void Update()
    {
        if (target == null)
        {
            if (useLaser)
            {
                lineRenderer.enabled = false;
                impactLight.enabled = false;
                impactEffect.Stop();
            }
            return;
        }
        else
        {
            targeting.LookAtCurrentTarget(target);
        }

        if (useLaser)
        {
            FireLaser();
        }
        else
        {
            // Should be placed in a Different Script
            if (fireCountdown <= 0f)
            {
                ShootBallistic();
                fireCountdown = 1f / fireRate;
            }
            fireCountdown -= Time.deltaTime;
        }
    }

    private void FireLaser()
    {
        targetHealth.TakeDamage(damageOverTime * Time.deltaTime);
        targetMovement.Slow(slowPercentage, slowDuration);

        lineRenderer.enabled = true;
        if (impactEffect.isStopped)
        {
            impactEffect.Play();
            impactLight.enabled = true;
        }
        lineRenderer.SetPosition(0, targeting.FirePointPosition);
        lineRenderer.SetPosition(1, target.position);

        var firePointDirection = targeting.FirePointPosition - target.position;
        impactEffect.transform.rotation = Quaternion.LookRotation(firePointDirection);
        impactEffect.transform.position = target.position + firePointDirection.normalized;
    }

    private void ShootBallistic()
    {
        var bulletInstance = Instantiate(bulletPrefab, targeting.FirePointPosition, targeting.FirePointRotation);
        bulletInstance.GetComponent<Bullet>().Seek(target);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, range);
    }
}
