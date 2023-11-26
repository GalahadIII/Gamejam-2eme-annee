using UnityEngine;
using UnityEngine.Tilemaps;

public class WallSpawnController : MonoBehaviour
{
    [Header("Controller")]
    [SerializeField] internal SpawnController spawnController;

    [Header("Object")]
    [SerializeField] internal Tilemap tilemap;
    [SerializeField] internal Tile wallTileData;
    [SerializeField] internal GameObject tileGameObject;

    [Header("Generation Settings")]
    [SerializeField] float seed = 0;
    [Range(0, 1)]
    [SerializeField] float modifier;

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

    private int[,] wallTable;
    int width;
    int height;
    GameObject containerGameObject;

    internal void WallSpawn()
    {
        containerGameObject = new GameObject("WallContainer");

        width = spawnController.width;
        height = spawnController.height;

        perlinCave();
    }

    internal void perlinCave()
    {
        wallTable = new int[width,height];
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                wallTable[x,y] = Mathf.RoundToInt(Mathf.PerlinNoise((x * modifier) + seed, (y * modifier) + seed));
            }
        }
        spawnController.wallTable = wallTable;

        Tile[] tiles = new Tile[spriteList.Length];
        for (int i = 0; i < spriteList.Length; i++)
        {
            Tile tile = ScriptableObject.CreateInstance<Tile>();
            tile.sprite = spriteList[i];
            tiles[i] = tile;
        }

        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                if(wallTable[x, y] == 1)
                {
                    int indexSprite = GetWallOrientation(x, y);
                    Sprite sprite = spriteList[indexSprite];
                    Tile tile = tiles[indexSprite];

                    tileGameObject.GetComponent<SpriteRenderer>().sprite = sprite;
                    Instantiate(tileGameObject, new Vector2(x, y), Quaternion.identity, containerGameObject.transform);

                    tilemap.SetTile(new Vector3Int(x, y, 0), tile);
                }
            }
        }

    }

    private int GetWallOrientation(int x, int y)
    {
        int binaryCloseWalls = 0;
        binaryCloseWalls += (x == 0 || wallTable[x - 1, y] == 1) ?          0b1000 : 0; // presence a gauche
        binaryCloseWalls += (y == (height-1) || wallTable[x, y+1] == 1) ?   0b0001 : 0; // presence en bas
        binaryCloseWalls += (x == (width-1) || wallTable[x + 1, y] == 1) ?  0b0010 : 0; // presence a droite
        binaryCloseWalls += (y == 0 || wallTable[x, y-1] == 1) ?            0b0100 : 0; // presence en haut

        return binaryCloseWalls;
    }
}
