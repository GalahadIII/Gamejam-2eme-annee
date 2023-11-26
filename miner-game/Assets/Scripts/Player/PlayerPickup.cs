using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class PlayerPickup : ObjectColliderDetector2D
{
    private Collider2D col;
    // Start is called before the first frame update
    void Start()
    {
        col = GetComponent<Collider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Objects.FirstOrDefault())
        {
            GameObject o = Objects.FirstOrDefault();

            if (o.CompareTag("Chunk"))
            {
                
            }
        }
    }
}
