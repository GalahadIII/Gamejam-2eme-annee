using UnityEngine;

public class MineralsSpawnController : MonoBehaviour
{
    [SerializeField] internal SpawnController spawnController;

    [SerializeField] GameObject[] mineralsList;

    [SerializeField] int Attempts = 50;

    internal void MineralsSpawn()
    {
        GameObject mineralContainer = new("MineralContainer");
        mineralContainer.transform.parent = transform;

        foreach (GameObject mineral in mineralsList)
        {
            Vector2Int mineralPosition = FindCoordinate(mineral);
            if (mineralPosition == Vector2Int.zero)
            {
                Debug.LogWarning($"Skipped placing {mineral}");
                continue;
            }
            // GameObject tile = mineral.GetComponent<MineralSpawnSetting>().GetTile();
            // Debug.Log($"{mineralPosition == null}");
            // Debug.Log($"{mineral} {mineralPosition} {mineralPosition?[0]} {mineralPosition?[1]}");
            Instantiate(mineral, (Vector3)(Vector2)mineralPosition, Quaternion.identity, mineralContainer.transform);
            spawnController.walls_tab[mineralPosition.x, mineralPosition.y] = 3;
        }
    }

    Vector2Int FindCoordinate(GameObject mineral)
    {
        int width = spawnController.width;
        int height = spawnController.height;
        int halfWidth = width / 2;
        int halfHeight = height / 2;

        MineralSpawnSetting mineralSettings = mineral.GetComponent<MineralSpawnSetting>();
        int minSpawnRadius = Mathf.Clamp(mineralSettings.GetMinSpawnRadius(), 0, Mathf.Min(halfWidth, halfHeight));
        int maxSpawnRadius = Mathf.Clamp(mineralSettings.GetMaxSpawnRadius(), 0, Mathf.Min(halfWidth, halfHeight));
        int spawnSpaceRadiusNeeded = mineralSettings.GetSpawnSpaceRadiusNeeded();
        int distanceMinBetweenMinerals = mineralSettings.GetDistanceMinBetweenMinerals();
        LayerMask layer = mineralSettings.GetLayer();

        // Debug.Log($"{minSpawnRadius} {maxSpawnRadius} {spawnSpaceRadiusNeeded} {distanceMinBetweenMinerals}");

        // generate a direction +-x +-y, then normalize
        for (int i = 0; i < Attempts; i++)
        {
            float randomX = Random.Range(-1000,1000);
            float randomY = Random.Range(-1000,1000);
            float randomL = Random.Range(minSpawnRadius, maxSpawnRadius);
            Vector2 position = new Vector2(randomX, randomY).normalized * randomL;
            Vector2Int result = new (Mathf.RoundToInt(position.x) + halfWidth, Mathf.RoundToInt(position.y) + halfHeight);
            // Debug.Log($"{randomX} {randomY} {randomL} {position} {result}");

            if (spawnController.checkEmptyZone.VerifierZone(result, spawnSpaceRadiusNeeded) &&
                spawnController.checkEmptyZone.VerifierZone(result, distanceMinBetweenMinerals, layer))
            {
                // result[0] = x; result[1] = y;
                return result;
            }
        }
        Debug.LogWarning($"Max attempts reached for {mineralSettings}");
        return Vector2Int.zero;

        // for (int x = halfWidth - maxSpawnRadius; x < halfWidth + maxSpawnRadius; x++)
        // {
        //     for (int y = halfHeight - maxSpawnRadius; y < halfHeight + maxSpawnRadius; y++)
        //     {
        //         if ((x > halfWidth - minSpawnRadius && x < halfWidth + minSpawnRadius) ||
        //             (y > halfHeight - minSpawnRadius && y < halfHeight + minSpawnRadius))
        //         {
        //             continue;
        //         }

        //         if (Random.Range(0,100) < 99f)
        //         {
        //             continue;
        //         }

        //         Vector2Int result = new(x, y);

        //     }
        // }
        // Debug.LogWarning($"Couldn't find a free spot for {mineralSettings}");
        // return Vector2Int.zero;
    }
}
