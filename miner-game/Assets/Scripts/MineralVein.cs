using UnityEngine;

public class MineralVein : MonoBehaviour
{

    [SerializeField]
    protected Transform visual;
    [SerializeField]
    protected float shakeTime = 0.3f;
    [SerializeField]
    protected float shakePower = 0.03f;

    [SerializeField] GameObject dropedChunk;

    protected float lastMine = 0;
    protected Vector3 originalPosition;

    #region mineral properties

    [Header("Mineral settings")]
    [SerializeField]
    protected float integrity = 3f;
    [SerializeField]
    protected float hardness = 1f;

    #endregion

    private void OnEnable()
    {
        lastMine = -shakeTime;
        originalPosition = visual.localPosition;
    }
    private void FixedUpdate()
    {
        if (lastMine + shakeTime > Time.time)
        {
            visual.localPosition = originalPosition + new Vector3(Random.Range(-shakePower, shakePower), Random.Range(-shakePower, shakePower), 0);
        }
    }

    public void Mine(float efficiency = 0)
    {
        Debug.Log($"MineralVein Mine {efficiency} {hardness} {integrity}");
        lastMine = Time.time;
        if (efficiency + 1 < hardness) return;
        integrity -= efficiency / hardness;

        if (integrity <= 0)
        {
            integrity = 0;
            Break();
        }
    }
    private void Break()
    {
        Debug.Log($"MineralVein Break");
        SpawnChunk();
    }

    private void SpawnChunk()
    {
        Vector3 spawnLocation = transform.position;

        float randX = Random.Range(-1f, 1f);
        float randY = Random.Range(-1f, 1f);

        Vector3 spawnOffset = new Vector3(randX, randY, 0f).normalized;

        Instantiate(dropedChunk, spawnLocation + spawnOffset, Quaternion.identity);
    }

}

struct MineralVeinSettings
{
    public float Hardness;
}

