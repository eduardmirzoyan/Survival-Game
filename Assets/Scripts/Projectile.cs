using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Rigidbody2D rigidbody2d;

    [Header("Debug")]
    [SerializeField, ReadOnly] private Vector2 velocity;
    [SerializeField, ReadOnly] private Vector3 startPosition;
    [SerializeField, ReadOnly] private float range;
    [SerializeField, ReadOnly] private int penetrationCount;
    [SerializeField, ReadOnly] private float damage;

    public void Initialize(Vector2 direction, ProjectileStats combatStats)
    {
        if (direction.magnitude > 1)
            throw new System.Exception($"DIRECTION MAG IS GREATER THAN 1: {direction.magnitude}");

        // print($"Dir: {direction}; Mag: {direction.magnitude}");

        velocity = direction * combatStats.shotSpeed;
        rigidbody2d.velocity = velocity;

        startPosition = transform.position;
        range = combatStats.attackRange;

        penetrationCount = combatStats.penetration;
        damage = combatStats.damage;

        Debug.DrawRay(transform.position, direction * combatStats.attackRange, Color.red, 5f);
    }

    private void Update()
    {
        if (Vector2.Distance(startPosition, transform.position) >= range)
            Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        var target = other.transform.parent;
        if (target.TryGetComponent(out Enemy enemy))
        {
            // print($"Hit: {target.name}");

            enemy.TakeDamage(damage);

            if (penetrationCount == 0)
                Destroy(gameObject);
            penetrationCount--;
        }
    }
}
