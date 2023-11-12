using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Camera cam;
    [SerializeField] private Transform playerTransform;
    [SerializeField] private GameObject enemyPrefab;

    [Header("Settings")]
    [SerializeField] private float delay;
    [SerializeField] private float spawnOffset;

    [Header("Debug")]
    [SerializeField, ReadOnly] private int enemyCount;

    private void Start()
    {
        cam = Camera.main;

        enemyCount = 0;

        StartCoroutine(SpawnEnemy(delay));
    }

    private IEnumerator SpawnEnemy(float delay)
    {
        yield return new WaitForSeconds(delay);

        while (true)
        {
            if (enemyCount == 1)
                break;

            // Spawn enemy somewhere off screen
            var worldPosition = Vector3.zero; // GetRandomPositionOffScreen();
            Instantiate(enemyPrefab, worldPosition, Quaternion.identity, transform).GetComponent<Enemy>().Initialize(playerTransform, enemyCount);
            enemyCount++;

            yield return new WaitForSeconds(delay);
        }
    }

    private Vector3 GetRandomPositionOffScreen()
    {
        float halfHeight = cam.orthographicSize;
        float halfWidth = cam.aspect * halfHeight;

        Vector3 position = cam.transform.position;
        position.z = 0f;

        // Gives small bias to corners
        int side = Random.Range(0, 4);
        switch (side)
        {
            case 0: // Top
                position.x += Random.Range(-halfWidth, halfWidth);
                position.y += halfHeight + spawnOffset;
                print(position);
                break;
            case 1: // Right
                position.x += halfWidth + spawnOffset;
                position.y += Random.Range(-halfHeight, halfHeight);
                break;
            case 2: // Down
                position.x += Random.Range(-halfWidth, halfWidth);
                position.y -= halfHeight + spawnOffset;
                print(position);
                break;
            case 3: // Left
                position.x -= halfWidth + spawnOffset;
                position.y += Random.Range(-halfHeight, halfHeight);
                break;
        }

        return position;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;

        float camHeight = 2 * cam.orthographicSize;
        float camWidth = cam.aspect * camHeight;

        Vector3 size = new Vector3(camWidth, camHeight) + new Vector3(spawnOffset, spawnOffset);
        Gizmos.DrawWireCube(cam.transform.position, size);
    }
}
