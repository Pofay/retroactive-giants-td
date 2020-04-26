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
    // Start is called before the first frame update
    void Start()
    {
        waypoints = FindObjectOfType<WaypointsContainer>();
        target = waypoints.points[0];
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Vector3 direction = target.position - transform.position;
        transform.Translate(direction.normalized * speed * Time.deltaTime, Space.World);
        if (Vector3.Distance(transform.position, target.position) <= 0.2f)
        {
            GetNextWaypoint();
        }
    }

    private void GetNextWaypoint()
    {
        if (wavePointIndex < waypoints.points.Length - 1)
        {
            wavePointIndex++;
            target = waypoints.points[wavePointIndex];
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
