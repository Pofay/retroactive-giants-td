using UnityEngine;

public abstract class Turret : MonoBehaviour
{
    [Header("Currency cost")]
    public int cost;

    [Header("Upgrade Attributes")]
    public int upgradeCost;
    public BuildableTurretDefinition upgradedVersion;

    [Header("General Combat Attributes")]
    public float range = 15f;

    protected TurretTargeting targeting;
    protected Transform target;

    public bool IsUpgradeable => upgradedVersion != null;

    public virtual void Awake()
    {
    }

    public virtual void Start()
    {
        InvokeRepeating("UpdateTarget", 0, 0.2f);
        targeting = GetComponent<TurretTargeting>();
    }

    protected virtual void UpdateTarget()
    {
        target = targeting.CalculateNextTarget(range);
    }

    protected abstract void Update();

    protected abstract void Shoot();

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, range);
    }
}
