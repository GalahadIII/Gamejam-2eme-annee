using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OresSpawn : MonoBehaviour
{
    [SerializeField] private GameObject[] spawnPoints;
    [SerializeField] private int numberMinerals;

    public void Start()
    {
        for (int i = 0; i < numberMinerals; i++) 
        {
            int randomSpawnPoint = Random.Range(0, spawnPoints.Length);
        }
    }
}
