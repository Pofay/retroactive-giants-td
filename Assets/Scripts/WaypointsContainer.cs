using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaypointsContainer : MonoBehaviour
{
    public Transform[] points;

    private void Awake()
    {
        InitializeWaypoints();
    }

    private void InitializeWaypoints()
    {
        points = new Transform[transform.childCount];
        for (var i = 0; i < points.Length; i++)
        {
            points[i] = transform.GetChild(i);
        }
    }
}
