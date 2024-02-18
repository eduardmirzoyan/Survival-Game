using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMelee : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] private float attackRate;
    [SerializeField] private float damage;

    [Header("Debug")]
    [SerializeField, ReadOnly] private float attackTimer;

    private void OnTriggerStay2D(Collider2D other)
    {
        if (attackTimer > 0)
        {
            attackTimer -= Time.deltaTime;
        }
        else
        {
            var target = other.transform.parent;
            if (target.TryGetComponent(out PlayerHealth playerHealth))
            {
                playerHealth.TakeDamage(damage);
            }
            attackTimer = attackRate;
        }
    }
}
