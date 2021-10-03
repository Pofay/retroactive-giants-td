using System.Collections;
using UnityEngine;

public interface IStatusEffect
{
    string Id { get; }
    bool IsActive { get; }

    IEnumerator ApplyEffect(GameObject target);
    void RefreshDuration();
}