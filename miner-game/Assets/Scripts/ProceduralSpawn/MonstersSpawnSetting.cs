using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonstersSpawnSetting : MonoBehaviour
{
    [Header("Monster Spawn Settings")]
    [Range(4, 200)]
    [SerializeField] int minSpawnRadius;
    [Range(10, 200)]
    [SerializeField] int maxSpawnRadius;
    [Range(1, 20)]
    [SerializeField] int spawnSpaceRadiusNeeded;
    [Range(1, 50)]
    [SerializeField] int distanceMinBetweenMinerals;
    [SerializeField] LayerMask layer;

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
}
