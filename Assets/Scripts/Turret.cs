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
    public float turnSpeed = 5f;
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
    private EnemyMovement targetMovement;
    private EnemyHealth targetHealth;

    [Header("Unity Setup Settings")]
    public string targetTag = "Enemy";
    public Transform partToRotate;
    public Transform firePoint;

    private Transform target;

    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("UpdateTarget", 0, 0.5f);
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

    // Update is called once per frame
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
            LookAtCurrentTarget();
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
        targetMovement.Slow(slowPercentage);

        lineRenderer.enabled = true;
        if (impactEffect.isStopped)
        {
            impactEffect.Play();
            impactLight.enabled = true;
        }
        lineRenderer.SetPosition(0, firePoint.position);
        lineRenderer.SetPosition(1, target.position);

        var firePointDirection = firePoint.position - target.position;
        impactEffect.transform.rotation = Quaternion.LookRotation(firePointDirection);
        impactEffect.transform.position = target.position + firePointDirection.normalized;
    }

    private void LookAtCurrentTarget()
    {
        var dir = target.position - transform.position;
        var lookRotation = Quaternion.LookRotation(dir);
        var rotation = Quaternion.Lerp(partToRotate.rotation, lookRotation, Time.deltaTime * turnSpeed).eulerAngles;
        partToRotate.rotation = Quaternion.Euler(0f, rotation.y, 0f);
    }

    private void ShootBallistic()
    {
        var bulletInstance = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        bulletInstance.GetComponent<Bullet>().Seek(target);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, range);
    }
}
