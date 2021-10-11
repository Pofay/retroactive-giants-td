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

    private Transform target;
    private IImpactEffect[] impactEffects;

    void Start()
    {
        impactEffects = GetComponents<IImpactEffect>();
    }

    public void Seek(Transform target)
    {
        this.target = target;
        StartCoroutine(SpawnAndSeek());
    }

    private IEnumerator SpawnAndSeek()
    {
        if (target == null)
        {
            Destroy(gameObject);
            yield return new WaitForEndOfFrame();
        }
        else
        {
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
        }
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
        SpawnVFX();
        DisableMesh();
        Destroy(gameObject, 1f);
    }

    private void SpawnVFX()
    {
        var effect = Instantiate(impactVFX, transform.position, transform.rotation);
        Destroy(effect, 2f);
    }

    private void DisableMesh()
    {
        GetComponentInChildren<MeshRenderer>().enabled = false;
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
