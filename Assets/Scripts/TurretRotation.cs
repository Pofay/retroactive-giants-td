using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretRotation : MonoBehaviour
{
    [Header("Unity Setup Settings")]
    public Transform partToRotate;
    public Transform firePoint;
    public float turnSpeed;

    public Vector3 FirePointPosition => firePoint.position;
    public Quaternion FirePointRotation => firePoint.rotation;

    public void LookAtCurrentTarget(Transform target)
    {
        var dir = target.position - transform.position;
        var lookRotation = Quaternion.LookRotation(dir);
        var rotation = Quaternion.Lerp(partToRotate.rotation, lookRotation, Time.deltaTime * turnSpeed).eulerAngles;
        partToRotate.rotation = Quaternion.Euler(0f, rotation.y, 0f);
    }

}
