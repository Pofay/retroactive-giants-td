using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class VFXEventChannel : MonoBehaviour
{
    public UnityEvent<Vector3, Quaternion, string> OnEventRaised;

    public static VFXEventChannel instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Debug.LogError("There are more than 1 instance of VFXEventChannel. Please check your scene.");
        }
    }

    public void RaiseEvent(Vector3 position, Quaternion rotation, string explosionName)
    {
        Debug.Log("Raised Event: VFX Spawn");
        OnEventRaised.Invoke(position, rotation, explosionName);
    }
}
