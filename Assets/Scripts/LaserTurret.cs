
using UnityEngine;

public class LaserTurret : Turret
{
    [Header("Laser Turret Attribute")]
    public LineRenderer lineRenderer;
    public ParticleSystem impactEffect;
    public Light impactLight;
    public int damageOverTime = 30;
    public StatusEffectFactory effectFactory;
    
    private EnemyMovement targetMovement;
    private EnemyHealth targetHealth;
    

    public override void Start()
    {
        base.Start();
    }

    protected override void UpdateTarget()
    {
        base.UpdateTarget();
        if (target != null)
        {
            targetMovement = target.GetComponent<EnemyMovement>();
            targetHealth = target.GetComponent<EnemyHealth>();
        }
    }

    protected override void Shoot()
    {
        targetHealth.TakeDamage(damageOverTime * Time.deltaTime);
        var statusEffectHandler = target.GetComponent<StatusEffectsHandler>();
        statusEffectHandler.AddEffect(effectFactory);

        lineRenderer.enabled = true;
        if (impactEffect.isStopped)
        {
            impactEffect.Play();
            impactLight.enabled = true;
        }
        lineRenderer.SetPosition(0, targeting.FirePointPosition);
        lineRenderer.SetPosition(1, target.position);

        var firePointDirection = targeting.FirePointPosition - target.position;
        impactEffect.transform.rotation = Quaternion.LookRotation(firePointDirection);
        impactEffect.transform.position = target.position + firePointDirection.normalized;
    }

    protected override void Update()
    {
        if (target == null)
        {
            lineRenderer.enabled = false;
            impactLight.enabled = false;
            impactEffect.Stop();
            return;
        }
        else
        {
            targeting.LookAtCurrentTarget(target);
        }
        Shoot();
    }
}
