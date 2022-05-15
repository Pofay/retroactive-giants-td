using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


public class VFXSpawner : MonoBehaviour
{
    [SerializeField] private VFXEventChannel eventChannel;

    private Dictionary<string, VFXPool> pools = new Dictionary<string, VFXPool>();

    private void Start()
    {
        SetupEventChannel();
        var vfxPools = GetComponents<VFXPool>();
        foreach (var pool in vfxPools)
        {
            pools.Add(pool.ExplosionName, pool);
        }
    }

    private void SetupEventChannel()
    {
        if (eventChannel == null)
        {
            eventChannel = VFXEventChannel.instance;
        }
        eventChannel.OnEventRaised.AddListener(SpawnVFX);
    }

    private void OnDisable()
    {
        eventChannel.OnEventRaised.RemoveListener(SpawnVFX);
    }

    private void OnDestroy()
    {
        pools.Clear();
        eventChannel.OnEventRaised.RemoveListener(SpawnVFX);
    }

    private void SpawnVFX(Vector3 position, Quaternion rotation, string explosionName)
    {
        Debug.Log("Spawning VFX");
        if (pools.ContainsKey(explosionName))
        {
            var vfxPool = pools[explosionName];
            if (vfxPool.IsReady)
            {
                var effect = vfxPool.GetVFX();
                effect.transform.position = position;
                effect.transform.rotation = rotation;
                StartCoroutine(ReturnInstanceOnExpiry(effect, vfxPool));
            }
        }
    }

    private IEnumerator ReturnInstanceOnExpiry(GameObject effect, VFXPool pool)
    {
        var duration = effect.GetComponent<ParticleSystem>().main.duration;
        yield return new WaitForSeconds(duration);
        pool.Return(effect);
    }
}
