using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWeapon : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private GameObject projectilePrefab;
    [SerializeField] private PlayerAttackTimerUI timerUI;
    [SerializeField] private Transform weaponHolderTransform;

    [Header("Settings")]
    [SerializeField] private ProjectileStats stats;

    [Header("Debug")]
    [SerializeField, ReadOnly] private bool isFiring;
    [SerializeField, ReadOnly] private float cooldown;

    private void Awake()
    {
        cooldown = 0f;
    }

    private void Update()
    {
        if (isFiring)
        {
            if (cooldown > 0f)
            {
                cooldown -= Time.deltaTime;
            }
            else
            {
                Fire();
                cooldown = stats.attackSpeed;
            }

            timerUI.UpdateValue(cooldown, stats.attackSpeed);
        }
    }

    public void Aim(Vector2 direction)
    {
        if (direction.magnitude > 1.001f)
            throw new System.Exception($"Input direction [{direction}] was not normalized: {direction.magnitude}");

        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        weaponHolderTransform.rotation = Quaternion.Euler(0, 0, angle);
    }

    public void Fire()
    {
        // Get direction the canon is in
        Vector2 direction = weaponHolderTransform.right;
        direction.Normalize();

        // Spawn bullet
        Instantiate(projectilePrefab, transform.position, Quaternion.identity).GetComponent<Projectile>().Initialize(direction, stats);
    }

    public void StartFiring()
    {
        isFiring = true;
        timerUI.SetVisibilty(true);
    }

    public void StopFiring()
    {
        isFiring = false;
        timerUI.SetVisibilty(false);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, stats.attackRange);
    }
}
