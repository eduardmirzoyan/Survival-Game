using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChunkManager : MonoBehaviour
{
    [Header("Debug")]
    [SerializeField] private float currentChunk;

    public static ChunkManager instance;
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

    private void Update()
    {
        // Check what chunk the player is

        // If they move to a new chunk, remove old chunks and render new ones

        // TODO
    }

}
