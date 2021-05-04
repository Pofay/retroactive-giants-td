using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Screams to be called TurretTargeting
public class Turret : MonoBehaviour
{

    [Header("Currency cost")]
    public int cost;

    [Header("Combat Attributes")]
    public float range = 15f;
    public float fireRate = 1f;
    public float turnSpeed = 5f;
    private float fireCountdown = 0f;

    [Header("Unity Setup Settings")]
    public string targetTag = "Enemy";
    public Transform partToRotate;
    public GameObject bulletPrefab;
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
    }

    // Update is called once per frame
    void Update()
    {
        if (target == null)
        {
            return;
        }
        else
        {
            LookAtCurrentTarget();
        }

        // Should be placed in a Different Script
        if (fireCountdown <= 0f)
        {
            Shoot();
            fireCountdown = 1f / fireRate;
        }
        fireCountdown -= Time.deltaTime;
    }

    private void LookAtCurrentTarget()
    {
        var dir = target.position - transform.position;
        var lookRotation = Quaternion.LookRotation(dir);
        var rotation = Quaternion.Lerp(partToRotate.rotation, lookRotation, Time.deltaTime * turnSpeed).eulerAngles;
        partToRotate.rotation = Quaternion.Euler(0f, rotation.y, 0f);
    }

    private void Shoot()
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
