using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class WorldGenerator : ScriptableObject
{
    [Header("Generation Details")]
    public int width;
    public int height;
    [Range(0, 100)] public int cullChance;
    public int numSteps;

    [Header("Seeding")]
    public bool useRandomSeed;
    public int seed;

    public int[,] Generate()
    {
        System.Random rng;

        if (useRandomSeed)
            rng = new System.Random();
        else
            rng = new System.Random(seed);

        // 0 - Floor | 1 - Wall
        int[,] map = new int[width, height];

        // Randomly fill area
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                if (x == 0 || x == width - 1 || y == 0 || y == height - 1)
                {
                    map[x, y] = 1;
                }
                else
                {
                    map[x, y] = (rng.Next(0, 100) < cullChance) ? 1 : 0;
                }
            }
        }

        // Simulate step
        for (int i = 0; i < numSteps; i++)
        {
            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                {
                    int neighbourWallTiles = GetSurroundingWallCount(x, y, map);

                    if (neighbourWallTiles > 4)
                        map[x, y] = 1;
                    else if (neighbourWallTiles < 4)
                        map[x, y] = 0;

                }
            }
        }

        return map;
    }

    private int GetSurroundingWallCount(int gridX, int gridY, int[,] map)
    {
        int wallCount = 0;
        for (int neighbourX = gridX - 1; neighbourX <= gridX + 1; neighbourX++)
        {
            for (int neighbourY = gridY - 1; neighbourY <= gridY + 1; neighbourY++)
            {
                if (neighbourX >= 0 && neighbourX < width && neighbourY >= 0 && neighbourY < height)
                {
                    if (neighbourX != gridX || neighbourY != gridY)
                    {
                        wallCount += map[neighbourX, neighbourY];
                    }
                }
                else
                {
                    wallCount++;
                }
            }
        }

        return wallCount;
    }
}
