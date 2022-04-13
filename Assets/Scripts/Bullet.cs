using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [Header("Unity Setup Settings")]
    public GameObject impactVFX;

    [Header("Game Attributes")]
    public float speed = 70f;
    public float lifetime = 1f;

    private Transform target;
    private IImpactEffect[] impactEffects;
    private MeshRenderer meshRenderer;
    private Light possiblyEmptyLight;
    private ProjectilePool pool;
    private bool isSeeking = false;

    void Awake()
    {
        impactEffects = GetComponents<IImpactEffect>();
        meshRenderer = GetComponentInChildren<MeshRenderer>();
        possiblyEmptyLight = GetComponentInChildren<Light>();
    }

    public void Seek(Transform target, ProjectilePool pool)
    {
        this.target = target;
        this.pool = pool;
        if (!isSeeking)
        {
            StartCoroutine(SpawnAndSeek());
        }
    }

    private IEnumerator SpawnAndSeek()
    {
        isSeeking = true;
        var direction = target.position - transform.position;
        var distanceThisFrame = speed * Time.deltaTime;

        while (!(direction.magnitude <= distanceThisFrame))
        {
            direction = target.position - transform.position;
            distanceThisFrame = speed * Time.deltaTime;
            MoveToTarget(direction, distanceThisFrame);
            yield return new WaitForEndOfFrame();
        }
        HitTarget();
        yield return new WaitForEndOfFrame();
        isSeeking = false;
    }

    private void MoveToTarget(Vector3 direction, float distanceThisFrame)
    {
        transform.Translate(direction.normalized * distanceThisFrame, Space.World);
        transform.LookAt(target);
    }

    private void HitTarget()
    {
        ApplyImpactEffects(target.gameObject);
        DisableMesh();
        DisableLights();
        this.pool.Return(this);
    }

    private void OnDisable()
    {
        ResetState();
    }

    private void ResetState()
    {
        this.target = null;
        isSeeking = false;
        this.meshRenderer.enabled = true;
        if (possiblyEmptyLight != null)
        {
            possiblyEmptyLight.enabled = true;
        }
    }

    private void DisableMesh()
    {
        this.meshRenderer.enabled = false;
    }

    private void DisableLights()
    {
        if (possiblyEmptyLight != null)
        {
            possiblyEmptyLight.enabled = false;
        }
    }

    void ApplyImpactEffects(GameObject target)
    {
        foreach (var impactEffect in impactEffects)
        {
            impactEffect.ApplyEffect(target);
        }
    }
}
