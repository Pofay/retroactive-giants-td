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
    public LayerMask targetMask;

    public Vector3 FirePointPosition => firePoint.position;
    public Quaternion FirePointRotation => firePoint.rotation;

    public Transform CalculateNextTarget(float range)
    {
        var possibleTargets = Physics.OverlapSphere(transform.position, range, targetMask);
        var shortestDistance = Mathf.Infinity;
        GameObject nearestTarget = null;
        foreach (var target in possibleTargets)
        {
            var distanceToEnemy = Vector3.Distance(transform.position, target.transform.position);
            if (distanceToEnemy < shortestDistance)
            {
                shortestDistance = distanceToEnemy;
                nearestTarget = target.gameObject;
            }
        }
        if (nearestTarget != null && IsInRange(shortestDistance, range)
            && nearestTarget.GetComponent<EnemyHealth>().isActiveAndEnabled)
        {
            return nearestTarget.transform;
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
