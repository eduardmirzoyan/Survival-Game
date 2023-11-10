using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Transform playerTransform;
    [SerializeField] private GameObject enemyPrefab;

    [Header("Settings")]
    [SerializeField] private float delay;

    [Header("Debug")]
    [SerializeField, ReadOnly] private int enemyCount;

    private void Start()
    {
        enemyCount = 0;

        StartCoroutine(DelayedSpawn(delay));
    }

    private IEnumerator DelayedSpawn(float delay)
    {
        yield return new WaitForSeconds(delay);

        while (true)
        {
            Instantiate(enemyPrefab, transform).GetComponent<Enemy>().Initialize(playerTransform, enemyCount);
            enemyCount++;

            yield return new WaitForSeconds(delay);
        }
    }
}
