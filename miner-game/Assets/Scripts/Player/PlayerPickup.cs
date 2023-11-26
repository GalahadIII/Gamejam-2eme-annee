using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using JetBrains.Annotations;
using UnityEngine;
using Debug = UnityEngine.Debug;

[RequireComponent(typeof(Collider2D))]
public class PlayerPickup : ObjectColliderDetector2D
{
    private Inventory _inventory;
    private Collider2D col;
    // Start is called before the first frame update
    void Start()
    {
        _inventory = GetComponentInParent<Inventory>();
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
                // ReSharper disable once Unity.PerformanceCriticalCodeInvocation
                IChunk chunk = o.GetComponent<IChunk>();
                
                switch(chunk.GetType().ToString())
                {
                    case "CoalChunk":
                        _inventory.AddItem();
                        break;
                    case "IronChunk":
                        _inventory.AddItem();
                        break;
                    case "GoldChunk":
                        _inventory.AddItem();
                        break;
                    case "DiamondChunk":
                        _inventory.AddItem();
                        break;
                }
                Destroy(o);
            }
        }
    }
}
