using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyMovement : MonoBehaviour
{
    private Transform target;
    private NavMeshAgent agent;
    public float speed = 10f;
    private float currentSpeed;

    private IDictionary<string, float> modifiers;

    public float BaseSpeed { get { return speed; } }

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        modifiers = new Dictionary<string, float>();
        target = FindObjectOfType<LivesRemover>().transform;
        agent.SetDestination(target.position);
        ChangeSpeed(speed);
    }

    public void AddModifier(string id, float value)
    {
        modifiers[id] = value;
        RecalculateCurrentSpeed();
    }

    private void RecalculateCurrentSpeed()
    {
        ChangeSpeed(BaseSpeed);
        foreach (var modifier in modifiers)
        {
            var modifierValue = modifier.Value;
            ChangeSpeed(currentSpeed + modifierValue);
        }
    }

    public void RemoveModifier(string effectId)
    {
        modifiers.Remove(effectId);
        RecalculateCurrentSpeed();
    }

    void FixedUpdate()
    {
        var lookRotation = CalculateRotationWithNoUpward(transform.forward);
        RotateToTarget(lookRotation);
    }

    private void ChangeSpeed(float speed)
    {
        currentSpeed = speed;
        agent.speed = currentSpeed;
    }

    private void RotateToTarget(Quaternion lookRotation)
    {
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5f);
    }

    private Quaternion CalculateRotationWithNoUpward(Vector3 direction)
    {
        return Quaternion.LookRotation(new Vector3(direction.x, 0f, direction.z));
    }

    private bool HasReachedCurrentWayPoint()
    {
        return Vector3.Distance(transform.position, target.position) <= 0.4;
    }

    public void ResetSpeed()
    {
        currentSpeed = speed;
    }
}
