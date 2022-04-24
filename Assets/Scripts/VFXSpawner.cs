using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


public class VFXSpawner : MonoBehaviour
{
    public VFXEventChannel eventChannel;

    private void OnEnable()
    {
        if (eventChannel == null)
        {
            return;
        }
        eventChannel.OnEventRaised.AddListener(SpawnVFX);
    }

    private void OnDisable()
    {
        if (eventChannel == null)
        {
            return;
        }
        eventChannel.OnEventRaised.RemoveListener(SpawnVFX);
    }

    private void SpawnVFX(Vector3 position, Quaternion rotation, GameObject prefab)
    {
        var effect = Instantiate(prefab, position, rotation);
        var duration = effect.GetComponent<ParticleSystem>().main.duration;
        Destroy(effect, duration);
    }
}
