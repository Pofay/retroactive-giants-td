using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretTargeting : MonoBehaviour
{
    [Header("Unity Setup Settings")]
    public float turnSpeed = 5;
    public Transform partToRotate;
    public Transform firePoint;
    public string targetTag = "Enemy";

    public Vector3 FirePointPosition => firePoint.position;
    public Quaternion FirePointRotation => firePoint.rotation;

    public Transform CalculateNextTarget(float range)
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
        if (nearestEnemy != null && IsInRange(shortestDistance, range)
            && nearestEnemy.GetComponent<EnemyHealth>().isActiveAndEnabled)
        {
            return nearestEnemy.transform;
        }
        else
        {
            return null;
        }
    }

    bool IsInRange(float distance, float range)
    {
        return distance <= range;
    }

    public void LookAtCurrentTarget(Transform target)
    {
        var dir = target.position - transform.position;
        var lookRotation = Quaternion.LookRotation(dir);
        var rotation = Quaternion.Lerp(partToRotate.rotation, lookRotation, Time.deltaTime * turnSpeed).eulerAngles;
        partToRotate.rotation = Quaternion.Euler(0f, rotation.y, 0f);
    }

}
