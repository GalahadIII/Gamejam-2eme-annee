using UnityEngine;

public class MineralsSpawnController : MonoBehaviour
{
    [SerializeField] internal SpawnController spawnController;

    [SerializeField] GameObject[] mineralsList;

    internal void MineralsSpawn()
    {
        GameObject containerGameObject = new GameObject("MineralContainer");
        containerGameObject.transform.parent = transform;

        foreach(GameObject mineral in mineralsList)
        {
            int[] mineralPosition = FindCoordinate(spawnController.walls_tab, mineral);
            GameObject tile = mineral.GetComponent<MineralSpawnSetting>().GetTile();
            Debug.Log($"{mineralPosition[0]} {mineralPosition[1]}");
            Instantiate(tile, new Vector2(mineralPosition[0], mineralPosition[1]), Quaternion.identity, containerGameObject.transform);
        }
    }

    int[] FindCoordinate(int[,] tabs, GameObject mineral)
    {
        int[] result = new int[2];
        int width = spawnController.width;
        int height = spawnController.height;
        int centerX = width / 2;
        int centerY = height / 2;

        int minSpawnRadius = mineral.GetComponent<MineralSpawnSetting>().GetMinSpawnRadius();
        int maxSpawnRadius = mineral.GetComponent<MineralSpawnSetting>().GetMaxSpawnRadius();
        int spawnSpaceRadiusNeeded = mineral.GetComponent<MineralSpawnSetting>().GetSpawnSpaceRadiusNeeded();
        int distanceMinBetweenMinerals = mineral.GetComponent<MineralSpawnSetting>().GetDistanceMinBetweenMinerals();
        int layer = mineral.GetComponent<MineralSpawnSetting>().GetLayer();

        for (int x = centerX - maxSpawnRadius; x < centerX + maxSpawnRadius; x++)
        {
            for (int y = centerY - maxSpawnRadius; y < centerY + maxSpawnRadius; y++)
            {
                if( x> centerX - minSpawnRadius && x < centerX - minSpawnRadius ||
                    y > centerY - minSpawnRadius && y < centerY - minSpawnRadius)
                {
                    continue;
                }

                if (spawnController.checkEmptyZone.VerifierZone(new int[] { x, y }, spawnSpaceRadiusNeeded) &&
                    spawnController.checkEmptyZone.VerifierZone(new int[] { x, y }, distanceMinBetweenMinerals, layer))
                {
                    result[0] = x; result[1] = y;
                    return result;
                }
            }
        }
        return null;
    }
}
