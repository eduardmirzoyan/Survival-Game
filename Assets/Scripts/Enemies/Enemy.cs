using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Movement))]
[RequireComponent(typeof(HitFlash))]
public class Enemy : MonoBehaviour
{
    [Header("Referenes")]
    [SerializeField] private Movement movement;
    [SerializeField] private HitFlash hitFlash;
    [SerializeField] private EnemyHudUI hudUI;

    [Header("Settings")]
    [SerializeField] private float health;
    [SerializeField] private float maxHealth;

    [Header("Debug")]
    [SerializeField, ReadOnly] private Transform target;
    [SerializeField] private bool isDisabled;

    private void Awake()
    {
        movement = GetComponent<Movement>();
        hitFlash = GetComponent<HitFlash>();
        hudUI = GetComponentInChildren<EnemyHudUI>();
    }

    public void Initialize(Transform target, int index)
    {
        this.target = target;
        maxHealth = health;

        hudUI.UpdateHealth(health, maxHealth);
    }

    private void Update()
    {
        if (isDisabled)
            return;

        if (target != null)
            ChaseTarget(target);
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

    #region Helpers

    private void ChaseTarget(Transform target)
    {
        // Get direction between target and self
        Vector2 direction = target.position - transform.position;
        direction.Normalize();

        movement.Move(direction);
    }

    #endregion
}
