using System.Collections;
using UnityEngine;

public class BurnEffect : IStatusEffect
{
    public string Id => "FLAMETHROWER_EFFECT";
    public bool IsActive { get; private set; }
    public float RunningDuration { get; private set; }

    private float damage;
    private float tickInterval;
    private float duration;

    public BurnEffect(float damage, float tickInterval, float duration) 
    {
        this.damage = damage;
        this.tickInterval = tickInterval;
        this.duration = duration;
    }

    public IEnumerator ApplyEffect(GameObject target)
    {
        RefreshDuration();
        var targetHealth = target.GetComponent<EnemyHealth>();
        IsActive = true;

        var instruction = new WaitForEndOfFrame();

        while(RunningDuration < duration)
        {
            yield return new WaitForSeconds(tickInterval);
            targetHealth.TakeDamage(damage);
            RunningDuration += tickInterval;
            yield return instruction;
        }

        IsActive = false;
        yield return null;
    }

    public void RefreshDuration()
    {
        RunningDuration = 0f;
    }
}
