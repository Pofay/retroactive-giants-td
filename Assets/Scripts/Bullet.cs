using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [Header("Unity Setup Settings")]
    public GameObject impactVFX;

    [Header("Game Attributes")]
    public float damage = 10;
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
    }

    void FixedUpdate()
    {
        if (target == null)
        {
            Destroy(gameObject);
        }
        else
        {
            var direction = target.position - transform.position;
            var distanceThisFrame = speed * Time.deltaTime;

            if (direction.magnitude <= distanceThisFrame)
            {
                HitTarget();
            }
            MoveToTarget(direction, distanceThisFrame);
        }
    }

    private void MoveToTarget(Vector3 direction, float distanceThisFrame)
    {
        transform.Translate(direction.normalized * distanceThisFrame, Space.World);
        transform.LookAt(target);
    }

    private void HitTarget()
    {
        var effect = Instantiate(impactVFX, transform.position, transform.rotation);
        Destroy(effect, 2f);

        if (explosiveRadius > 0f)
        {
            Explode();
        }
        else
        {
            Damage(target);
        }
        Destroy(gameObject);
    }

    void Explode()
    {
        var colliders = Physics.OverlapSphere(transform.position, explosiveRadius);
        foreach (var collider in colliders)
        {
            if (collider.CompareTag("Enemy"))
            {
                Damage(collider.transform);
            }
        }
    }

    void Damage(Transform enemy)
    {
        var enemyHealth = enemy.GetComponent<EnemyHealth>();
        if (enemyHealth != null)
        {
            foreach(var impactEffect in impactEffects)
            {
                impactEffect.ApplyEffect(target);
            }
            enemyHealth.TakeDamage(damage);
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, explosiveRadius);
    }
}
