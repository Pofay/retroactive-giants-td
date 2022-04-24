using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(menuName = "System/Event Channels/VFX", order = 1)]
public class VFXEventChannel : ScriptableObject
{
    public UnityEvent<Vector3, Quaternion, GameObject> OnEventRaised;

    public void RaiseEvent(Vector3 position, Quaternion rotation, GameObject prefab)
    {
        OnEventRaised?.Invoke(position, rotation, prefab);
    }
}
