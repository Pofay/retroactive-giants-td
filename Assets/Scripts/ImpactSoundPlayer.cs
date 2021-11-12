using System;
using UnityEngine;

public class ImpactSoundPlayer : MonoBehaviour
{
    public ImpactSound[] impactSFX;

    public static ImpactSoundPlayer instance;

    void Awake()
    {
        instance = this;
        foreach (var s in impactSFX)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;
            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
        }
    }

    public void Play(string name)
    {
        var s = Array.Find(impactSFX, sound => sound.name.Equals(name));
        s.source.Play();
    }
}
