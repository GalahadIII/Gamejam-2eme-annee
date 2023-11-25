using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestVerifierZone : MonoBehaviour
{
    void Update()
    {
        VerifierZone();
    }

    bool VerifierZone()
    {
        Collider2D collider = Physics2D.OverlapCircle(new Vector2(0, 0), 5);
        if (collider is not null) Debug.Log(collider);

        return collider is null;
    }
}
