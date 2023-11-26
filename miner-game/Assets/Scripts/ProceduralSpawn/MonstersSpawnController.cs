using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonstersSpawnController : MonoBehaviour
{
    [SerializeField] SpawnController spawnController;
    [SerializeField] GameObject[] monstersList;

    [SerializeField] int Attempts = 50;

    internal void MonstersSpawn()
    {
        GameObject monsterContainer = new("MonsterContainer");

        foreach (GameObject monster in monstersList)
        {
            Vector2Int monsterPosition = FindCoordinate(monster);
            if (monsterPosition == Vector2Int.zero)
            {
                Debug.LogWarning($"Skipped placing {monster}");
                continue;
            }
            Instantiate(monster, (Vector3)(Vector2)monsterPosition, Quaternion.identity, monsterContainer.transform);
            spawnController.wallTable[monsterPosition.x, monsterPosition.y] = 3;
        }
    }

    Vector2Int FindCoordinate(GameObject monster)
    {
        int width = spawnController.width;
        int height = spawnController.height;
        int halfWidth = width / 2;
        int halfHeight = height / 2;

        MonstersSpawnSetting monsterSettings = monster.GetComponent<MonstersSpawnSetting>();
        int minSpawnRadius = Mathf.Clamp(monsterSettings.GetMinSpawnRadius(), 0, Mathf.Min(halfWidth, halfHeight));
        int maxSpawnRadius = Mathf.Clamp(monsterSettings.GetMaxSpawnRadius(), 0, Mathf.Min(halfWidth, halfHeight));
        int spawnSpaceRadiusNeeded = monsterSettings.GetSpawnSpaceRadiusNeeded();
        int distanceMinBetweenMinerals = monsterSettings.GetDistanceMinBetweenMinerals();
        LayerMask layer = monsterSettings.GetLayer();

        // generate a direction +-x +-y, then normalize
        for (int i = 0; i < Attempts; i++)
        {
            float randomX = Random.Range(-1000, 1000);
            float randomY = Random.Range(-1000, 1000);
            float randomL = Random.Range(minSpawnRadius, maxSpawnRadius);
            Vector2 position = new Vector2(randomX, randomY).normalized * randomL;
            Vector2Int result = new(Mathf.RoundToInt(position.x) + halfWidth, Mathf.RoundToInt(position.y) + halfHeight);

            if (spawnController.checkEmptyZone.VerifierZone(result, spawnSpaceRadiusNeeded) &&
                spawnController.checkEmptyZone.VerifierZone(result, distanceMinBetweenMinerals, layer))
            {
                // result[0] = x; result[1] = y;
                return result;
            }
        }
        Debug.LogWarning($"Max attempts reached for {monsterSettings}");
        return Vector2Int.zero;
    }
}
