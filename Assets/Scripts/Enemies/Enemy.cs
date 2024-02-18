using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    [Header("Referenes")]
    [SerializeField] private NavMeshAgent agent;
    [SerializeField] private HitFlash hitFlash;
    [SerializeField] private EnemyHudUI hudUI;

    [Header("Settings")]
    [SerializeField] private float health;
    [SerializeField] private float maxHealth;

    [Header("Debug")]
    [SerializeField, ReadOnly] private Transform target;
    [SerializeField] private bool disable;

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
        maxHealth = health;

        hudUI.UpdateHealth(health, maxHealth);
    }

    private void Update()
    {
        if (disable)
            return;

        if (target != null)
            agent.SetDestination(target.position);
    }

    public void TakeDamage(float damage)
    {
        hitFlash.Flash();

        health -= damage;
        if (health <= 0)
        {
            EnemyManager.instance.KillEnemy();
            Destroy(gameObject);
        }
        else
        {
            hudUI.UpdateHealth(health, maxHealth);
        }
    }
}
