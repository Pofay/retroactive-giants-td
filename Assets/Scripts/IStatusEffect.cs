using System.Collections;
using UnityEngine;

public interface IStatusEffect
{
    string Id { get; }
    float RunningDuration { get; }
    bool IsActive { get; }

    IEnumerator ApplyEffect(GameObject target);
    void RefreshDuration();
}