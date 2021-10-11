using System;
using UnityEngine;

public class ParticleEmittingTurret : Turret
{
    [SerializeField]
    private ParticleSystem flames;
    private AudioSource audioSource;

    [Header("Flamethrower Settings")]
    public float damage = 5f;
    public float damageInterval = 0.7f;

    public override void Start()
    {
        base.Start();
        audioSource = GetComponent<AudioSource>();
    }

    protected override void Shoot()
    {
        if (!flames.isPlaying)
        {
            flames.Play();
        }

        if (!audioSource.isPlaying)
        {
            audioSource.Play();
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
        audioSource.Stop();
    }
}
