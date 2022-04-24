using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VFXImpactEffect : MonoBehaviour, IImpactEffect
{
    public VFXEventChannel eventChannel;

    [SerializeField] private string explosionName;

    public void ApplyEffect(GameObject target)
    {
        ShowVFX();
    }

    private void ShowVFX()
    {
        Debug.Log("Called");
        eventChannel.RaiseEvent(transform.position, transform.rotation, explosionName);
    }

}
