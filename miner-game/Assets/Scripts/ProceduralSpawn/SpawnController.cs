using NavMeshPlus.Components;
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
    [SerializeField] internal GameObject area;

    [Header("Generation Settings")]
    [SerializeField] internal int width;
    [SerializeField] internal int height;

    [SerializeField] internal int[,] wallTable;

    private void Start()
    {
        wallTable = new int[width, height];

        area.transform.position = new Vector3(width/2, height/2);
        area.transform.localScale = new Vector3(width, height, 1);

        wallSpawnController.WallSpawn();
        forgeSpawnController.ForgeSpawn();
        mineralsSpawnController.MineralsSpawn();
        monstersSpawnController.MonstersSpawn();
        meshSurface.BuildNavMesh();
    }
}
