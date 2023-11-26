using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class PlayerPickup : ObjectColliderDetector
{
    private Inventory _inventory;
    private Collider2D col;

    [SerializeField] private List<Item> listItem;
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
                        _inventory.AddItem(listItem[0]);
                        break;
                    case "IronChunk":
                        _inventory.AddItem(listItem[1]);
                        break;
                    case "GoldChunk":
                        _inventory.AddItem(listItem[2]);
                        break;
                    case "DiamondChunk":
                        _inventory.AddItem(listItem[3]);
                        break;
                }
                Destroy(o);
            }
        }
    }
}
