using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForgeSpawnController : MonoBehaviour
{
    [SerializeField] internal SpawnController spawnController;

    [Header("Spawn Settings")]
    [Range(5, 100)]
    [SerializeField] int spawnRadius;
    [Range(2, 15)]
    [SerializeField] int spawnSpaceRadiusNeeded;
    [SerializeField] GameObject tile;
    [SerializeField] int maxWallReload;
    [SerializeField] Transform player;


    internal void ForgeSpawn()
    {
        Vector2Int coordinate = FindPlaceToSpawn(spawnController.wallTable);
        int reloadCount = 0;
        while (coordinate == Vector2Int.zero && reloadCount < maxWallReload)
        {
            spawnController.wallSpawnController.perlinCave();
            coordinate = FindPlaceToSpawn(spawnController.wallTable);
            reloadCount++;
        }

        if (reloadCount >= maxWallReload)
        {
            throw new System.Exception("Too much map reload");
        }

        Instantiate(tile, (Vector2)coordinate, Quaternion.identity);
        spawnController.wallTable[coordinate.x, coordinate.y] = 2;
        player.position = new Vector3(coordinate.x, coordinate.y + 2);
    }

    Vector2Int FindPlaceToSpawn(int[,] tabs)
    {
        Vector2Int result = Vector2Int.zero;
        int width = spawnController.width;
        int height = spawnController.height;
        int centerX = width / 2;
        int centerY = height / 2;

        if (2 * (spawnRadius + spawnSpaceRadiusNeeded) > width || 2 * (spawnRadius + spawnSpaceRadiusNeeded) > height)
        {
            throw new System.Exception("Radius and/or space needed value not correct");
        }
        for (int x = centerX - spawnRadius; x < centerX + spawnRadius; x++)
        {
            for (int y = centerY - spawnRadius; y < centerY + spawnRadius; y++)
            {
                result = new(x, y);
                if (spawnController.checkEmptyZone.VerifierZone(result, spawnSpaceRadiusNeeded))
                {
                    return result;
                }
            }
        }
        Debug.LogWarning($"Couldn't find a free spot for forge");
        return result;
    }
}
