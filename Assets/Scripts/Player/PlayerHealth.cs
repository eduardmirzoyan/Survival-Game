using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private HitFlash hitFlash;

    [Header("Settings")]
    [SerializeField] private float maxHealth;

    [Header("Debug")]
    [SerializeField, ReadOnly] private float currentHealth;

    private void Start()
    {
        currentHealth = maxHealth;
    }

    public void TakeDamage(float damage)
    {
        print($"Player took {damage} damage.");

        hitFlash.Flash();

        currentHealth -= damage;
        if (currentHealth <= 0)
        {
            print("YOU LOSE!");

            Destroy(gameObject);

            GameManager.instance.Restart();
        }
    }
}
