using UnityEngine;

public class SlowingImpactEffect : MonoBehaviour, IImpactEffect
{
    [Header("Slowing Effect settings")]
    public float slowDuration = 3f;
    [Range(0f, 1f)] public float slowPercentage = 0.3f;

    public void ApplyEffect(Transform target)
    {
        var targetMovement = target.GetComponent<EnemyMovement>();
        if (targetMovement != null)
        {
            targetMovement.Slow(slowPercentage, slowDuration);
        }
    }
}
