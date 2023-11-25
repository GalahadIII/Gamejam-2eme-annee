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

    [Header("Generation Settings")]
    [SerializeField] internal int width;
    [SerializeField] internal int height;

    [SerializeField] internal int[,] walls_tab;

    private void Start()
    {
        walls_tab = new int[width, height];
    }
}
