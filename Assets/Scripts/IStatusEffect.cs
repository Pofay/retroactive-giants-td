using UnityEngine;

public interface IStatusEffect 
{
    string Id { get; }
    float RunningDuration { get; }

    void Tick(GameObject target, float time);
    void ResetStatus();
    void RefreshDuration();
}