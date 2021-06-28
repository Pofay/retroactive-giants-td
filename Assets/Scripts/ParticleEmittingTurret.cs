using System;
using UnityEngine;

public class ParticleEmittingTurret : Turret
{
    [SerializeField] private ParticleSystem flames;
    [Header("Flamethrower Settings")]
    public float damage = 5f;
    public float damageInterval = 0.7f;

    protected override void Shoot()
    {
        if (!flames.isPlaying)
        {
            flames.Play();
        }
    }

    protected override void Update()
    {
        if (target == null)
        {
            DisableFlames();
            return;
        }
        else
        {
            targeting.LookAtCurrentTarget(target);
            Shoot();
        }
    }

    private void DisableFlames()
    {
        flames.Stop();
    }
}
