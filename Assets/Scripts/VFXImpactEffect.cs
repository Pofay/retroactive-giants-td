using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VFXImpactEffect : MonoBehaviour, IImpactEffect
{
    [SerializeField] private VFXEventChannel eventChannel;
    [SerializeField] private string explosionName;

    private void Start()
    {
        eventChannel = VFXEventChannel.instance;
    }

    public void ApplyEffect(GameObject target)
    {
        ShowVFX();
    }

    private void ShowVFX()
    {
        eventChannel.RaiseEvent(transform.position, transform.rotation, explosionName);
    }
}
