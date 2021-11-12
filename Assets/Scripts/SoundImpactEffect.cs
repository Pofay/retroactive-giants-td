using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundImpactEffect : MonoBehaviour, IImpactEffect
{
    [SerializeField]
    private string impactSFXName;
    

    public void ApplyEffect(GameObject target)
    {
        ImpactSoundPlayer.instance.Play(impactSFXName);
    }
}
