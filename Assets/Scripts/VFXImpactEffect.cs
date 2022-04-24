using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VFXImpactEffect : MonoBehaviour, IImpactEffect
{
    public VFXEventChannel eventChannel;

    [SerializeField] private GameObject prefab;

    public void ApplyEffect(GameObject target)
    {
        ShowVFX();
    }

    private void ShowVFX()
    {
        eventChannel.RaiseEvent(transform.position, transform.rotation, prefab);
    }

}
