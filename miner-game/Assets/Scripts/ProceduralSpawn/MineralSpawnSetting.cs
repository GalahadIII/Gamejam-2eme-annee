using UnityEngine;

public class MineralSpawnSetting : MonoBehaviour
{
    [Header("Mineral Spawn Settings")]
    [Range(2, 25)]
    [SerializeField] int minSpawnRadius;
    [Range(5, 50)]
    [SerializeField] int maxSpawnRadius;
    [Range(2, 15)]
    [SerializeField] int spawnSpaceRadiusNeeded;
    [Range(1, 50)]
    [SerializeField] int distanceMinBetweenMinerals;
    [SerializeField] LayerMask layer;
    // [SerializeField] GameObject tile;

    internal int GetMinSpawnRadius()
    {
        return minSpawnRadius;
    }

    internal int GetMaxSpawnRadius()
    {
        return maxSpawnRadius;
    }

    internal int GetSpawnSpaceRadiusNeeded()
    {
        return spawnSpaceRadiusNeeded;
    }

    internal int GetDistanceMinBetweenMinerals()
    {
        return distanceMinBetweenMinerals;
    }

    internal LayerMask GetLayer()
    {
        return layer;
    }

    // internal GameObject GetTile()
    // {
    //     return tile;
    // }
}
