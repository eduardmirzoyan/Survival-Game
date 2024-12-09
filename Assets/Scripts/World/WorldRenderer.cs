using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using NavMeshPlus.Components;

public class WorldRenderer : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private NavMeshSurface surface;
    [SerializeField] private Tilemap wallTilemap;
    [SerializeField] private Tile wallTile;

    [Header("Settings")]
    [SerializeField] private WorldGenerator worldGenerator;

    public static WorldRenderer instance;
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

    public void GenerateWorld(int[,] map, Transform player)
    {
        for (int i = 0; i < map.GetLength(0); i++)
        {
            for (int j = 0; j < map.GetLength(1); j++)
            {
                Vector3Int position = new Vector3Int(i, j);
                switch (map[i, j])
                {
                    case 0: // Floor

                        // Do nothing.

                        break;
                    case 1: // Wall

                        wallTilemap.SetTile(position, wallTile);

                        break;
                }
            }
        }

        player.transform.position = new Vector3(worldGenerator.width / 2f, worldGenerator.height / 2f);
        wallTilemap.CompressBounds();
    }

    public void GenerateNavMap()
    {
        surface.BuildNavMesh();
    }
}
