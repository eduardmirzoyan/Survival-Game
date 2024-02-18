using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Camera cam;
    [SerializeField] private Transform playerTransform;
    [SerializeField] private GameObject enemyPrefab;
    [SerializeField] private PlayerHudUI hudUI;

    [Header("Settings")]
    [SerializeField] private float delay;
    [SerializeField] private float spawnOffset;
    [SerializeField] private int enemyCap;
    [SerializeField] private int killGoal;

    [Header("Debug")]
    [SerializeField, ReadOnly] private float minSpawnRadius;
    [SerializeField, ReadOnly] private int enemyCount;
    [SerializeField, ReadOnly] private int killCount;

    private int[,] map;
    private Coroutine coroutine;

    public static EnemyManager instance;
    private void Awake()
    {
        // Singleton Logic
        if (instance != null)
        {
            Destroy(gameObject);
            return;
        }

        instance = this;
    }

    private void Start()
    {
        cam = Camera.main;
        float screenHeight = 2 * cam.orthographicSize;
        float screenWidth = cam.aspect * screenHeight;
        minSpawnRadius = Mathf.Sqrt(screenHeight * screenHeight + screenWidth * screenWidth) / 2f;

        enemyCount = 0;
    }

    public void Initialize(int[,] map)
    {
        this.map = map;

        // FIXME later
        hudUI.UpdateEnemyCount(enemyCount, enemyCap);
        hudUI.UpdateKillGoalLabel(killCount, killGoal);
    }

    public void StartSpawning()
    {
        coroutine = StartCoroutine(SpawnEnemy(delay));
    }
    public void StopSpawning()
    {
        if (coroutine != null)
            StopCoroutine(coroutine);
    }

    private IEnumerator SpawnEnemy(float delay)
    {
        yield return new WaitForSeconds(delay);

        while (true)
        {
            if (enemyCount < enemyCap)
            {
                // Spawn enemy somewhere off screen
                var worldPosition = GetRandomPositionOffScreen(); // new Vector3(map.GetLength(0) / 2f, map.GetLength(1) / 2f); //
                Instantiate(enemyPrefab, worldPosition, Quaternion.identity, transform).GetComponent<Enemy>().Initialize(playerTransform, enemyCount);
                enemyCount++;

                // FIXME later
                hudUI.UpdateEnemyCount(enemyCount, enemyCap);
            }

            yield return new WaitForSeconds(delay);
        }
    }

    private Vector3 GetRandomPositionOffScreen()
    {
        Vector3 position = Vector3.zero;
        position.z = 0f;

        do
        {
            int x;
            int y;
            do
            {
                x = Random.Range(0, map.GetLength(0));
                y = Random.Range(0, map.GetLength(1));
            }
            while (map[x, y] == 1);

            position = new Vector3(x, y);

        } while (Vector3.Distance(cam.transform.position, position) <= minSpawnRadius);

        return position;
    }

    public void KillEnemy()
    {
        enemyCount -= 1;
        killCount += 1;
        if (killCount == killGoal)
        {
            // Win Game!
            print("YOU WIN!");

            GameManager.instance.Restart();
        }

        // FIXME later
        hudUI.UpdateEnemyCount(enemyCount, enemyCap);
        hudUI.UpdateKillGoalLabel(killCount, killGoal);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;

        float screenHeight = 2 * cam.orthographicSize;
        float screenWidth = cam.aspect * screenHeight;

        float radius = Mathf.Sqrt(screenHeight * screenHeight + screenWidth * screenWidth) / 2f;

        Gizmos.DrawWireSphere(cam.transform.position, radius);
    }
}
