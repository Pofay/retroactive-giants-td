using System.Collections;
using UnityEngine;

public class CryoSlowEffect : IStatusEffect
{
    public string Id => "CRYO_SLOW_EFFECT";
    public bool IsActive { get; private set; }

    private float slowPercentage;
    private float duration;
    private EnemyMovement targetMovement;

    public float RunningDuration { get; private set; }

    public CryoSlowEffect(GameObject target, float slowPercentage, float duration)
    {
        this.targetMovement = target.GetComponent<EnemyMovement>();
        this.slowPercentage = slowPercentage;
        this.duration = duration;
        this.RunningDuration = duration;
        this.IsActive = false;
    }

    public IEnumerator ApplyEffect(GameObject target)
    {
        RefreshDuration();

        IsActive = true;
        targetMovement.AddModifier(Id, -(targetMovement.BaseSpeed * (slowPercentage)));

        while (RunningDuration > 0)
        {
            RunningDuration -= Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }

        RemoveAppliedEffect(targetMovement.gameObject);
        yield return null;
    }

    public void RemoveAppliedEffect(GameObject target)
    {
        targetMovement.RemoveModifier(Id);
        IsActive = false;
    }

    public void RefreshDuration()
    {
        RunningDuration = duration;
    }
}
