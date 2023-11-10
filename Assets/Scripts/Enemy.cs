using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    [SerializeField] private NavMeshAgent agent;

    [SerializeField] private Transform target;

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.updateRotation = false;
        agent.updateUpAxis = false;
    }

    public void Initialize(Transform target, int index)
    {
        this.target = target;
        agent.avoidancePriority = index;
    }

    private void Update()
    {
        if (target != null)
            agent.SetDestination(target.position);
    }
}
