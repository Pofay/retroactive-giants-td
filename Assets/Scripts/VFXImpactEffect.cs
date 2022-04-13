using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VFXImpactEffect : MonoBehaviour, IImpactEffect
{
    [SerializeField] private GameObject impactVFX;

    public void ApplyEffect(GameObject target)
    {
        ShowVFX();
    }

    private void ShowVFX()
    {
        var effect = Instantiate(impactVFX, transform.position, transform.rotation);
        var duration = effect.GetComponent<ParticleSystem>().main.duration;
        Destroy(effect, duration);
    }

}
