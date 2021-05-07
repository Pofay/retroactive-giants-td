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

    //TO MOVE, Laser Specific Attributes
    public int damageOverTime = 30;
    [Range(0f, 1f)]public float slowPercentage = 0.2f;
    public float slowDuration = 2f;
    private EnemyMovement targetMovement;
    private EnemyHealth targetHealth;

    [Header("Unity Setup Settings")]
    public string targetTag = "Enemy";

    private Transform target;
    private TurretRotation turretRotation;

    void Start()
    {
        InvokeRepeating("UpdateTarget", 0, 0.5f);
        turretRotation = GetComponent<TurretRotation>();
    }

    void UpdateTarget()
    {
        var enemies = GameObject.FindGameObjectsWithTag(targetTag);
        var shortestDistance = Mathf.Infinity;
        GameObject nearestEnemy = null;
        // Bloody hell.. can't refactor
        // It's all about finding the nearest enemy that's withing range.
        foreach (var enemy in enemies)
        {
            var distanceToEnemy = Vector3.Distance(transform.position, enemy.transform.position);
            if (distanceToEnemy < shortestDistance)
            {
                shortestDistance = distanceToEnemy;
                nearestEnemy = enemy;
            }
        }
        if (nearestEnemy != null && IsInRange(shortestDistance))
        {
            LockOn(nearestEnemy.transform);
        }
        else
        {
            target = null;
        }
    }

    bool IsInRange(float distance)
    {
        return distance <= range;
    }

    void LockOn(Transform enemy)
    {
        target = enemy;
        // Laser specific
        targetHealth = enemy.GetComponent<EnemyHealth>();
        targetMovement = enemy.GetComponent<EnemyMovement>();
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
            turretRotation.LookAtCurrentTarget(target);
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
        lineRenderer.SetPosition(0, turretRotation.FirePointPosition);
        lineRenderer.SetPosition(1, target.position);

        var firePointDirection = turretRotation.FirePointPosition - target.position;
        impactEffect.transform.rotation = Quaternion.LookRotation(firePointDirection);
        impactEffect.transform.position = target.position + firePointDirection.normalized;
    }

    private void ShootBallistic()
    {
        var bulletInstance = Instantiate(bulletPrefab, turretRotation.FirePointPosition, turretRotation.FirePointRotation);
        bulletInstance.GetComponent<Bullet>().Seek(target);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, range);
    }
}
