using System;
using System.Collections;
using UnityEngine;

public class SoundPlayer : MonoBehaviour
{
    public ImpactSound[] impactSFX;
    public ImpactSound[] startingSFX;

    public static SoundPlayer instance;

    void Awake()
    {
        instance = this;
        InstantiateSFX(impactSFX);
        InstantiateSFX(startingSFX);
    }

    private void InstantiateSFX(ImpactSound[] sounds)
    {
        foreach (var s in sounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;
            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
        }
    }

    public void PlayStartingSFX(string name)
    {
        PlaySound(startingSFX, name);
    }

    public void PlayImpactSFX(string name)
    {
        PlaySound(impactSFX, name);
    }
    private void PlaySound(ImpactSound[] soundSource, string name)
    {
        var s = Array.Find(soundSource, sound => sound.name.Equals(name));
        s.source.Play();
    }
}
