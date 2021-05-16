using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    public float speed = 10f;
    private Transform target;

    private int wavePointIndex = 0;
    private WaypointsContainer waypoints;
    private float currentSpeed;

    private bool isSlowed;

    void Awake()
    {
        currentSpeed = speed;
        isSlowed = false;
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
        if (HasReachedCurrentWayPoint())
        {
            SetNextWaypoint();
        }
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

    public void Slow(float slowPercentage, float time)
    {
        if (!isSlowed)
        {
            isSlowed = true;
            StartCoroutine(TriggerSlowDebuff(slowPercentage, time));
        }
    }

    private IEnumerator TriggerSlowDebuff(float amount, float time)
    {
        currentSpeed = speed * (1f - amount);
        yield return new WaitForSeconds(time);
        ResetMovement();
        yield return null;
    }

    public void ResetMovement()
    {
        isSlowed = false;
        currentSpeed = speed;
    }
}
