using UnityEngine;

public class CryoSlowEffect : IStatusEffect
{
    public string Id => "CRYO_SLOW_EFFECT";

    private float slowPercentage;
    private float duration;
    private EnemyMovement targetMovement;

    public float RunningDuration { get; private set; }

    
    public CryoSlowEffect(float slowPercentage, float duration)
    {
        this.slowPercentage = slowPercentage;
        this.duration = duration;
        this.RunningDuration = duration;
    }

    public void Tick(GameObject target, float time)
    {
        targetMovement = target.GetComponent<EnemyMovement>();
        RunningDuration -= time;
        targetMovement.CurrentSpeed = targetMovement.BaseSpeed * (1f - slowPercentage);
    }

    public void RefreshDuration()
    {
        RunningDuration = duration;
    }

    public void ResetStatus()
    {
        targetMovement.ResetSpeed();
    }
}
