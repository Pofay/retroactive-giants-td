using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [Header("Unity Setup Settings")]
    public float speed = 70f;
    public GameObject impactEffect;

    private Transform target;

    public void Seek(Transform target)
    {
        this.target = target;
    }

    // Update is called once per frame
    void Update()
    {
        if (target == null)
        {
            Destroy(gameObject);
        }

        var direction = target.position - transform.position;
        var distanceThisFrame = speed * Time.deltaTime;

        if (direction.magnitude <= distanceThisFrame)
        {
            HitTarget();
        }
        MoveToTarget(direction, distanceThisFrame);
    }

    private void MoveToTarget(Vector3 direction, float distanceThisFrame)
    {
        transform.Translate(direction.normalized * distanceThisFrame, Space.World);
    }

    private void HitTarget()
    {
        var effect = Instantiate(impactEffect, transform.position, transform.rotation);
        Destroy(effect, 2f);
        Destroy(target.gameObject);
        Destroy(gameObject);
    }
}
