using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [Header("Unity Setup Settings")]
    public GameObject impactVFX;

    [Header("Game Attributes")]
    public float explosiveRadius = 0f;
    public float speed = 70f;
    public float lifetime = 1f;

    private Transform target;
    private IImpactEffect[] impactEffects;
    private ProjectilePool pool;
    private bool isSeeking = false;

    void Awake()
    {
        impactEffects = GetComponents<IImpactEffect>();
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
        if (explosiveRadius > 0f)
        {
            Explode();
        }
        else
        {
            ApplyImpactEffects(target.gameObject);
        }
        ShowVFX();
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
        GetComponentInChildren<MeshRenderer>().enabled = true;

        var light = GetComponentInChildren<Light>();
        if (light != null)
        {
            light.enabled = true;
        }
    }

    private void ShowVFX()
    {
        var effect = Instantiate(impactVFX, transform.position, transform.rotation);
        var duration = effect.GetComponent<ParticleSystem>().main.duration;
        Destroy(effect, duration);
    }

    private void DisableMesh()
    {
        GetComponentInChildren<MeshRenderer>().enabled = false;
    }

    private void DisableLights()
    {
        var light = GetComponentInChildren<Light>();
        if (light != null)
        {
            light.enabled = false;
        }
    }

    void Explode()
    {
        var colliders = Physics.OverlapSphere(transform.position, explosiveRadius);
        foreach (var collider in colliders)
        {
            if (collider.CompareTag("Enemy"))
            {
                ApplyImpactEffects(collider.gameObject);
            }
        }
    }

    void ApplyImpactEffects(GameObject target)
    {
        foreach (var impactEffect in impactEffects)
        {
            impactEffect.ApplyEffect(target);
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, explosiveRadius);
    }
}
