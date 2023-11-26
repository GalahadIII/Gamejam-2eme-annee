using NavMeshPlus.Components;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnController : MonoBehaviour
{
    #region Singleton
    public static SpawnController instance;

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Debug.LogWarning("More than one instance of SpawnController found !");
            return;
        }

        instance = this;
    }
    #endregion

    [SerializeField] internal WallSpawnController wallSpawnController;
    [SerializeField] internal ForgeSpawnController forgeSpawnController;
    [SerializeField] internal MineralsSpawnController mineralsSpawnController;
    [SerializeField] internal MonstersSpawnController monstersSpawnController;
    [SerializeField] internal CheckEmptyZone checkEmptyZone;

    [SerializeField] internal NavMeshSurface meshSurface;
    [SerializeField] internal GameObject player;

    [Header("Generation Settings")]
    [SerializeField] internal int width;
    [SerializeField] internal int height;

    [SerializeField] internal int[,] wallTable;

    private void Start()
    {
        wallTable = new int[width, height];

        wallSpawnController.WallSpawn();
        forgeSpawnController.ForgeSpawn();
        mineralsSpawnController.MineralsSpawn();
        monstersSpawnController.MonstersSpawn();
        meshSurface.BuildNavMesh();
    }
}
