using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Camera cam;
    [SerializeField] private Transform playerTransform;
    [SerializeField] private GameObject enemyPrefab;
    [SerializeField] private PlayerHudUI hudUI;

    [Header("Settings")]
    [SerializeField] private float spawnDelay;
    [SerializeField] private float spawnRadiusPadding;
    [SerializeField] private int enemyCap;
    [SerializeField] private int killGoal;

    [Header("Debug")]
    [SerializeField, ReadOnly] private float minSpawnRadius;
    [SerializeField, ReadOnly] private int enemyCount;
    [SerializeField, ReadOnly] private int killCount;

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

    public void Initialize()
    {
        // FIXME later
        hudUI.UpdateEnemyCount(enemyCount, enemyCap);
        hudUI.UpdateKillGoalLabel(killCount, killGoal);
    }

    public void StartSpawning()
    {
        coroutine = StartCoroutine(SpawnEnemy(spawnDelay, spawnRadiusPadding));
    }

    public void StopSpawning()
    {
        if (coroutine != null)
            StopCoroutine(coroutine);
    }

    private IEnumerator SpawnEnemy(float delay, float padding)
    {
        yield return new WaitForSeconds(delay);

        while (true)
        {
            if (enemyCount < enemyCap)
            {
                // Spawn enemy somewhere off screen
                Vector2 camPostion = CameraManager.instance.GetCameraWorldPosition();
                float distance = minSpawnRadius + padding;
                var worldPosition = camPostion + Random.insideUnitCircle * distance;
                Instantiate(enemyPrefab, worldPosition, Quaternion.identity, transform).GetComponent<Enemy>().Initialize(playerTransform, enemyCount);
                enemyCount++;

                // FIXME later
                hudUI.UpdateEnemyCount(enemyCount, enemyCap);
            }

            yield return new WaitForSeconds(delay);
        }
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
        float padding = spawnRadiusPadding;

        Vector3 camPosition = cam.transform.position;
        camPosition.z = 0f;

        Gizmos.DrawWireSphere(camPosition, radius + padding);
    }
}
