using UnityEngine;

public class MineralVein : MonoBehaviour
{

    #region mineral properties

    protected float hardness = 1f;

    #endregion

    private void OnEnable() { }
    private void FixedUpdate() { }

    public void Mine(float efficiency = 0)
    {
        Debug.Log($"MINE MineralVein {gameObject.name} {efficiency}");
    }

}

struct MineralVeinSettings
{
    public float Hardness;
}

