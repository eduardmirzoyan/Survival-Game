using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Movement : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Rigidbody2D rigidbody2d;

    [Header("Settings")]
    [SerializeField] private MovementSettings settings;

    [Header("Debug")]
    [SerializeField, ReadOnly] private Vector2 velocity;

    private void Awake()
    {
        rigidbody2d = GetComponent<Rigidbody2D>();
    }

    public void Move(Vector2 direction)
    {
        if (direction.magnitude > 1.001f)
            throw new System.Exception($"Input direction [{direction}] was not normalized: {direction.magnitude}");

        if (direction == Vector2.zero)
            velocity = Vector2.MoveTowards(velocity, Vector2.zero, settings.deceleration * Time.deltaTime);
        else
            velocity = Vector2.MoveTowards(velocity, direction * settings.maxSpeed, settings.acceleration * Time.deltaTime);

        rigidbody2d.velocity = velocity;
    }
}
