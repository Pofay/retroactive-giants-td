using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallisticTurret : Turret
{
    [Header("Ballistic Turret Attribute(default)")]
    public float fireRate = 1f;
    private float fireCountdown = 0f;
    public GameObject bulletPrefab;

    protected override void Shoot()
    {
        var bulletInstance = Instantiate(bulletPrefab, targeting.FirePointPosition, targeting.FirePointRotation);
        bulletInstance.GetComponent<Bullet>().Seek(target);
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
