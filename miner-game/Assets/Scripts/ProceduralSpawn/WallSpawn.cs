using UnityEngine;

public class WallSpawn : MonoBehaviour
{
    [SerializeField] int width, height;
    [SerializeField] float seed;
    [Range(0, 1)]
    [SerializeField] float modifier;
    [SerializeField] GameObject tile;

    private void Start()
    {
        seed = Random.Range(-100000,100000);
        perlinCave();
    }

    void perlinCave()
    {
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                int spawnPoint = Mathf.RoundToInt(Mathf.PerlinNoise((x * modifier) + seed, (y * modifier) + seed));
                if(spawnPoint == 1)
                {
                    Instantiate(tile, new Vector2(x, y), Quaternion.identity);
                }
            }
        }
    }
}
