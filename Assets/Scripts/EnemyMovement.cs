﻿using System;
using System.Collections;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    public float speed = 10f;
    private Transform target;

    private int wavePointIndex = 0;
    private WaypointsContainer waypoints;
    private float currentSpeed;

    public float CurrentSpeed
    {
        get { return currentSpeed; }
        set { currentSpeed = value; }
    }

    public float BaseSpeed { get { return speed; } }

    void Awake()
    {
        currentSpeed = speed;
    }

    void Start()
    {
        waypoints = FindObjectOfType<WaypointsContainer>();
        target = waypoints.points[0];
    }

    void FixedUpdate()
    {
        Vector3 direction = target.position - transform.position;
        transform.Translate(direction.normalized * currentSpeed * Time.deltaTime, Space.World);
        var lookRotation = CalculateRotationWithNoUpward(direction);
        RotateToTarget(lookRotation);
        if (HasReachedCurrentWayPoint())
        {
            SetNextWaypoint();
        }
    }

    private void RotateToTarget(Quaternion lookRotation)
    {
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5f);
    }

    private Quaternion CalculateRotationWithNoUpward(Vector3 direction)
    {
        return Quaternion.LookRotation(new Vector3(direction.x, 0f, direction.z));
    }

    private bool HasReachedCurrentWayPoint()
    {
        return Vector3.Distance(transform.position, target.position) <= 0.4;
    }

    private bool HasNotReachedLastWaypoint()
    {
        return wavePointIndex < waypoints.points.Length - 1;
    }

    private void SetNextWaypoint()
    {
        if (HasNotReachedLastWaypoint())
        {
            SetTargetToNextWaypoint();
        }
        else
        {
            if (target.GetComponent<LivesRemover>() != null)
            {
                target.GetComponent<LivesRemover>().RemoveLives(1);
                this.gameObject.SetActive(false);
            }
        }
    }

    private void SetTargetToNextWaypoint()
    {
        wavePointIndex++;
        target = waypoints.points[wavePointIndex];
    }

    public void ResetSpeed()
    {
        currentSpeed = speed;
    }
}
