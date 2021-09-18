using System.Collections;
using UnityEngine;

public class LaserSlowEffect : IStatusEffect
{
    public string Id => "LASER_SLOW_EFFECT";
    public float RunningDuration { get; private set; }
    public bool IsActive { get; private set; }

    private float slowPercentage;
    private float duration;

    public LaserSlowEffect(float slowPercentage, float duration)
    {
        this.slowPercentage = slowPercentage;
        this.duration = duration;
        this.RunningDuration = duration;
        this.IsActive = false;
    }

    public IEnumerator ApplyEffect(GameObject target)
    {
        RefreshDuration();
        var targetMovement = target.GetComponent<EnemyMovement>();

        IsActive = true;
        targetMovement.AddModifier(Id, -(targetMovement.BaseSpeed * (slowPercentage)));

        while (RunningDuration > 0)
        {
            RunningDuration -= Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }

        RemoveAppliedEffect(targetMovement);
        yield return null;
    }

    private void RemoveAppliedEffect(EnemyMovement targetMovement)
    {
        targetMovement.RemoveModifier(Id);
        IsActive = false;
    }

    public void RefreshDuration()
    {
        RunningDuration = duration;
    }
}
