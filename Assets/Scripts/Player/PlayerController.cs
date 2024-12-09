using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Movement))]
[RequireComponent(typeof(PlayerWeapon))]
public class PlayerController : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] private Movement movement;
    [SerializeField] private PlayerWeapon weapon;

    const int LEFT_CLICK = 0;

    private void Awake()
    {
        movement = GetComponent<Movement>();
        weapon = GetComponent<PlayerWeapon>();
    }

    private void Update()
    {
        HandleMovement();
        HandleWeapon();
    }

    private void HandleMovement()
    {
        float moveX = Input.GetAxisRaw("Horizontal");
        float moveY = Input.GetAxisRaw("Vertical");
        Vector2 moveDirection = new Vector2(moveX, moveY).normalized;

        movement.Move(moveDirection);
    }

    private void HandleWeapon()
    {
        // Get position of mouse
        Vector3 mousePosition = CameraManager.instance.GetMouseWorldPosition();
        Vector2 aimDirection = (mousePosition - transform.position).normalized;

        // Aim in direction
        weapon.Aim(aimDirection);

        // Fire when prompted
        if (Input.GetMouseButtonDown(LEFT_CLICK))
        {
            weapon.StartFiring();
        }
        else if (Input.GetMouseButtonUp(LEFT_CLICK))
        {
            weapon.StopFiring();
        }
    }
}
