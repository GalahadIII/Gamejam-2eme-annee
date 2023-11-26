using UnityEngine;

public class CheckEmptyZone : MonoBehaviour
{
    [SerializeField] internal SpawnController spawnController;

    internal bool VerifierZone(Vector2Int position, int spawnSpaceRadiusNeeded)
    {
        Collider2D collider = Physics2D.OverlapCircle(position, spawnSpaceRadiusNeeded);

        return collider is null;
    }

    internal bool VerifierZone(Vector2Int position, int spawnSpaceRadiusNeeded, LayerMask layer)
    {
        Collider2D collider = Physics2D.OverlapCircle(position, spawnSpaceRadiusNeeded, layer);

        return collider is null;
    }
}
