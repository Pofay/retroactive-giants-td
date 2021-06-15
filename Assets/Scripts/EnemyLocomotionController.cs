using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyLocomotionController : MonoBehaviour
{
    public float locomotionSmoothing = 0.1f;

    private Animator animator;
    private EnemyMovement movement;

    void Start()
    {
        animator = GetComponentInChildren<Animator>();
        movement = GetComponent<EnemyMovement>();
    }

    void Update()
    {
        var speed = transform.position.magnitude / movement.speed;
        animator.SetFloat("speed", speed, locomotionSmoothing, Time.deltaTime); 
    }
}
