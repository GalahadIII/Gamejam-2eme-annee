using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForgeSpawnController : MonoBehaviour
{
    [SerializeField] internal SpawnController spawnController;

    [Header("Spawn Settings")]
    [Range(25, 100)]
    [SerializeField] int spawnRadius;
    [Range(2, 15)]
    [SerializeField] int spawnSpaceRadiusNeeded;
    [SerializeField] GameObject tile;
    [SerializeField] int maxWallReload;


    void Start()
    {
        spawnForge();
    }

    int[] findPlaceToSpawn(int[,] tabs)
    {
        int[] result = new int[2];
        int width = spawnController.width;
        int height = spawnController.height;
        int centerX = width / 2;
        int centerY = height / 2;

        if (2 * (spawnRadius + spawnSpaceRadiusNeeded) > width || 2 * (spawnRadius + spawnSpaceRadiusNeeded) > height)
        {
            throw new System.Exception("Radius and/or space needed value not correct");
        }
        for (int x = centerX- spawnRadius; x < centerX + spawnRadius; x++)
        {
            for (int y = centerY - spawnRadius; y < centerY + spawnRadius; y++)
            {
                if (VerifierZone(new int[] {x,y}))
                {
                    result[0] = x; result[1] = y;
                    return result;
                }
            }
        }
        return null;
    }

    void spawnForge()
    {
        int[] coordinate = findPlaceToSpawn(spawnController.walls_tab);
        int reloadCount = 0;
        while (coordinate is null && reloadCount < maxWallReload)
        {
            spawnController.wallSpawnController.perlinCave();
            coordinate = findPlaceToSpawn(spawnController.walls_tab);
            reloadCount++;
        }

        if (reloadCount >= maxWallReload)
        {
            throw new System.Exception("Too much map reload");
        }

        Instantiate(tile, new Vector2(coordinate[0], coordinate[1]), Quaternion.identity);
    }

    bool VerifierZone(int[] position)
    {
        Collider2D collider = Physics2D.OverlapCircle(new Vector2(position[0], position[1]), spawnSpaceRadiusNeeded);

        return collider is null;
    }
}
