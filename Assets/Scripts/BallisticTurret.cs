using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallisticTurret : Turret
{
    [Header("Ballistic Turret Attribute(default)")]
    public float fireRate = 1f;
    public GameObject bulletPrefab;
    public GameObject projectilePoolGO;

    private float fireCountdown = 0f;
    private ProjectilePool projectilePool;

    public override void Start()
    {
        base.Start();
        var poolParent = GameObject.FindGameObjectWithTag("BulletPoolParent");
        var poolGO = Instantiate(projectilePoolGO, this.transform.position, Quaternion.identity, poolParent.transform);
        projectilePool = poolGO.GetComponent<ProjectilePool>();
    }

    protected override void Shoot()
    {
        if (target != null && projectilePool.IsReady)
        {
            var bulletInstance = projectilePool.GetProjectile();
            bulletInstance.transform.position = targeting.FirePointPosition;
            bulletInstance.transform.rotation = targeting.FirePointRotation;
            bulletInstance.GetComponent<Bullet>().Seek(target, projectilePool);
        }
    }

    protected override void Update()
    {
        if (target == null)
        {
            return;
        }
        else if (!target.gameObject.activeSelf)
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
