using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Transform player;

    [Header("Settings")]
    [SerializeField] private WorldGenerator worldGenerator;

    public static GameManager instance;
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
        StartCoroutine(DelayedStart());
    }

    private IEnumerator DelayedStart()
    {
        player = FindObjectOfType<Movement>().transform;

        var map = worldGenerator.Generate();
        //WorldRenderer.instance.GenerateWorld(map, player);
        //WorldRenderer.instance.GenerateNavMap();
        EnemyManager.instance.Initialize();

        yield return new WaitForEndOfFrame();

        EnemyManager.instance.StartSpawning();
        TransitionManager.instance.OpenScene();
    }

    public void Restart()
    {
        EnemyManager.instance.StopSpawning();
        TransitionManager.instance.ReloadScene();
    }
}
