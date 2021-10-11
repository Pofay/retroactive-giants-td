using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundImpactEffect : MonoBehaviour, IImpactEffect
{
    private AudioSource soundFX;
    [SerializeField]
    private AudioClip audioClip;

    void Start()
    {
            soundFX = GetComponent<AudioSource>();
    }

    public void ApplyEffect(GameObject target)
    {
        soundFX.PlayOneShot(audioClip);
    }
}
