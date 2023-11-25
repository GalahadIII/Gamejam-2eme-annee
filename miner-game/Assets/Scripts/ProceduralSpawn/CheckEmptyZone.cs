using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckEmptyZone : MonoBehaviour
{
    [SerializeField] internal SpawnController spawnController;

    internal bool VerifierZone(int[] position, int spawnSpaceRadiusNeeded)
    {
        Collider2D collider = Physics2D.OverlapCircle(new Vector2(position[0], position[1]), spawnSpaceRadiusNeeded);

        return collider is null;
    }

    internal bool VerifierZone(int[] position, int spawnSpaceRadiusNeeded, int layer)
    {
        Collider2D collider = Physics2D.OverlapCircle(new Vector2(position[0], position[1]), spawnSpaceRadiusNeeded);

        return collider is null;
    }
}
