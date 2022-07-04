using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [Header("Game Attributes")]
    public float speed = 70f;
    public float lifetime = 1f;
    public string initialSoundName;

    private Transform target;
    private IImpactEffect[] impactEffects;
    private ProjectilePool pool;
    private bool isSeeking = false;

    private void OnEnable()
    {
        SoundPlayer.instance.PlayStartingSFX(initialSoundName);
    }

    void Start()
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
        ApplyImpactEffects(target.gameObject);
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
    }

    void ApplyImpactEffects(GameObject target)
    {
        foreach (var impactEffect in impactEffects)
        {
            impactEffect.ApplyEffect(target);
        }
    }
}
