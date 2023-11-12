using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Rigidbody2D rigidbody2d;

    [Header("Debug")]
    [SerializeField, ReadOnly] private Vector2 velocity;
    [SerializeField, ReadOnly] private Vector3 endPosition;

    public void Initialize(Vector2 direction, float shotSpeed, float attackRange)
    {
        if (direction.magnitude > 1)
            throw new System.Exception("DIRECTION IS NOT NORMALZIED?!");

        velocity = direction * shotSpeed;
        rigidbody2d.velocity = velocity;

        // Calculate end point
        endPosition = transform.position + (Vector3)direction * attackRange;
    }

    private void Update()
    {
        if (Vector3.Distance(transform.position, endPosition) <= 0.1f)
            Destroy(gameObject);
    }
}
