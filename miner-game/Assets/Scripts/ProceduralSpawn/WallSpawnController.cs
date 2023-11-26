using UnityEngine;
using UnityEngine.Tilemaps;

public class WallSpawnController : MonoBehaviour
{
    [Header("Controller")]
    [SerializeField] internal SpawnController spawnController;

    [Header("Object")]
    [SerializeField] internal Tilemap tilemap;

    [Header("Generation Settings")]
    [SerializeField] float seed;
    [Range(0, 1)]
    [SerializeField] float modifier;
    [SerializeField] GameObject tileGameObject;

    [Header("Sprite list")]
    [SerializeField] Sprite[] spriteList;
    // 0 = centre seul
    // 1 = bas seul
    // 2 = gauche seul
    // 3 = bas-gauche
    // 4 = haut seul
    // 5 = gauche-droite
    // 6 = haut-gauche
    // 7 = gauche
    // 8 = droite seul
    // 9 = bas-droite
    // 10 = haut-bas
    // 11 = bas
    // 12 = haut-droite
    // 13 = droite
    // 14 = haut
    // 15 = centre

    private int[,] walls_tab;
    int width;
    int height;
    GameObject containerGameObject;

    internal void WallSpawn()
    {
        containerGameObject = new GameObject("WallContainer");
        containerGameObject.transform.parent = transform;

        width = spawnController.width; 
        height = spawnController.height;
        walls_tab = spawnController.walls_tab;
        seed = Random.Range(-100000,100000);
        perlinCave();
    }

    internal void perlinCave()
    {
        walls_tab = new int[width,height];
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                walls_tab[x,y] = Mathf.RoundToInt(Mathf.PerlinNoise((x * modifier) + seed, (y * modifier) + seed));
            }
        }

        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                if(walls_tab[x, y] == 1)
                {
                    tileGameObject.GetComponent<SpriteRenderer>().sprite = GetWallOrientation(x, y);
                    Instantiate(tileGameObject, new Vector2(x, y), Quaternion.identity, containerGameObject.transform);
                    Tile tile = ScriptableObject.CreateInstance<Tile>();
                    tile.sprite = GetWallOrientation(x, y);
                    tilemap.SetTile(new Vector3Int(x, y, 0), tile);
                }
            }
        }

        spawnController.walls_tab = walls_tab;
    }

    private Sprite GetWallOrientation(int x, int y)
    {
        int binaryCloseWalls = 0;
        binaryCloseWalls += (x == 0 || walls_tab[x - 1, y] == 1) ?          0b1000 : 0; // presence a gauche
        binaryCloseWalls += (y == (height-1) || walls_tab[x, y+1] == 1) ?   0b0001 : 0; // presence en bas
        binaryCloseWalls += (x == (width-1) || walls_tab[x + 1, y] == 1) ?  0b0010 : 0; // presence a droite
        binaryCloseWalls += (y == 0 || walls_tab[x, y-1] == 1) ?            0b0100 : 0; // presence en haut

        return spriteList[binaryCloseWalls];
    }
}
