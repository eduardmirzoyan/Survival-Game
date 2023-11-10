using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Rigidbody2D rigidbody2d;

    [Header("Settings")]
    [SerializeField] private MovementSettings settings;

    [Header("Debug")]
    [SerializeField, ReadOnly] private Vector2 velocity;

    private void Update()
    {
        float moveX = Input.GetAxisRaw("Horizontal");
        float moveY = Input.GetAxisRaw("Vertical");
        Vector2 direction = new Vector2(moveX, moveY).normalized;

        velocity = Vector2.MoveTowards(velocity, direction * settings.maxSpeed, settings.acceleration * Time.deltaTime);

        rigidbody2d.velocity = velocity;
    }
}
