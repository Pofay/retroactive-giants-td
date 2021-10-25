using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallisticTurret : Turret
{
    [Header("Ballistic Turret Attribute(default)")]
    public float fireRate = 1f;
    public GameObject bulletPrefab;

    private float fireCountdown = 0f;

    protected override void Shoot()
    {
        if (target != null)
        {
            var bulletInstance = Instantiate(bulletPrefab, targeting.FirePointPosition, targeting.FirePointRotation);
            bulletInstance.GetComponent<Bullet>().Seek(target);
        }
    }

    protected override void Update()
    {
        if (target == null)
        {
            return;
        }
        else
        {
            targeting.LookAtCurrentTarget(target);
        }

        if (fireCountdown <= 0f)
        {
            Shoot();
            fireCountdown = 1f / fireRate;
        }
        fireCountdown -= Time.deltaTime;
    }
}
