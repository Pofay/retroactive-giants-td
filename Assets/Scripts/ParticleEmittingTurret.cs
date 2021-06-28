using System;
using UnityEngine;

public class ParticleEmittingTurret : Turret
{
    [SerializeField] private ParticleSystem flames;

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
