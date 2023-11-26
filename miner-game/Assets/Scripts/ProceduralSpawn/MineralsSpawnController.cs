using System;
using UnityEngine;

public class MineralsSpawnController : MonoBehaviour
{
    [SerializeField] internal SpawnController spawnController;

    [SerializeField] GameObject[] mineralsList;

    internal void MineralsSpawn()
    {
        GameObject mineralContainer = new("MineralContainer");
        mineralContainer.transform.parent = transform;

        foreach (GameObject mineral in mineralsList)
        {
            Vector2Int mineralPosition = FindCoordinate(mineral);
            // GameObject tile = mineral.GetComponent<MineralSpawnSetting>().GetTile();
            // Debug.Log($"{mineralPosition == null}");
            // Debug.Log($"{mineral} {mineralPosition} {mineralPosition?[0]} {mineralPosition?[1]}");
            Instantiate(mineral, (Vector3)(Vector2)mineralPosition, Quaternion.identity, mineralContainer.transform);
            spawnController.walls_tab[mineralPosition.x, mineralPosition.y] = 3;
        }
    }

    Vector2Int FindCoordinate(GameObject mineral)
    {
        Vector2Int result = Vector2Int.zero;
        int width = spawnController.width;
        int height = spawnController.height;
        int centerX = width / 2;
        int centerY = height / 2;

        MineralSpawnSetting mineralSettings = mineral.GetComponent<MineralSpawnSetting>();
        int minSpawnRadius = mineralSettings.GetMinSpawnRadius();
        int maxSpawnRadius = mineralSettings.GetMaxSpawnRadius();
        int spawnSpaceRadiusNeeded = mineralSettings.GetSpawnSpaceRadiusNeeded();
        int distanceMinBetweenMinerals = mineralSettings.GetDistanceMinBetweenMinerals();
        LayerMask layer = mineralSettings.GetLayer();

        // Debug.Log($"{minSpawnRadius} {maxSpawnRadius} {spawnSpaceRadiusNeeded} {distanceMinBetweenMinerals}");

        for (int x = centerX - maxSpawnRadius; x < centerX + maxSpawnRadius; x++)
        {
            for (int y = centerY - maxSpawnRadius; y < centerY + maxSpawnRadius; y++)
            {
                if ((x > centerX - minSpawnRadius && x < centerX + minSpawnRadius) ||
                    (y > centerY - minSpawnRadius && y < centerY + minSpawnRadius))
                {
                    continue;
                }

                result = new(x, y);

                if (spawnController.checkEmptyZone.VerifierZone(result, spawnSpaceRadiusNeeded) &&
                    spawnController.checkEmptyZone.VerifierZone(result, distanceMinBetweenMinerals, layer))
                {
                    // result[0] = x; result[1] = y;
                    return result;
                }
            }
        }
        Debug.LogWarning($"Couldn't find a free spot for {mineralSettings}");
        return result;
    }
}
