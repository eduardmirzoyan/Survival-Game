using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombat : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private CircleCollider2D collider2d;
    [SerializeField] private GameObject projectilePrefab;

    [Header("Settings")]
    [SerializeField] private CombatStats combatStats;

    [Header("Debug")]
    [SerializeField] private List<Transform> targetsInRange;
    [SerializeField, ReadOnly] private float attackCooldown;

    private void Start()
    {
        targetsInRange = new List<Transform>();
        collider2d.radius = combatStats.attackRange;
    }

    private void Update()
    {
        if (attackCooldown > 0)
        {
            attackCooldown -= Time.deltaTime;
        }
        else
        {
            AttackClosestTarget();
            attackCooldown = combatStats.attackSpeed;
        }

    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        var target = other.transform.parent;
        if (!targetsInRange.Contains(target))
        {
            targetsInRange.Add(target);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        var target = other.transform.parent;
        bool sucess = targetsInRange.Remove(target);
        if (!sucess)
        {
            throw new System.Exception("CANNOT FIND TARGET?");
        }
    }

    private void AttackClosestTarget()
    {
        if (targetsInRange.Count == 0)
            return;

        float closestDistane = float.MaxValue;
        Transform enemy = null;
        foreach (var target in targetsInRange)
        {
            float distance = Vector3.Distance(transform.position, target.position);
            if (distance < closestDistane)
            {
                closestDistane = distance;
                enemy = target;
            }
        }

        Vector3 direction = enemy.position - transform.position;
        direction.Normalize();

        Instantiate(projectilePrefab, transform.position, Quaternion.identity).GetComponent<Projectile>().Initialize(direction, combatStats.shotSpeed, combatStats.attackRange);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, combatStats.attackRange);
    }
}
