using UnityEngine;

public class ObjectColliderDetector2D : ObjectColliderDetector
{

    private void OnCollisionEnter2D(Collision2D col)
    {
        // Debug.Log($"ColliderDetector2D OnCollisionEnter2D {col.gameObject.name}");
        AddObject(col.gameObject);
    }
    private void OnCollisionExit2D(Collision2D col)
    {
        // Debug.Log($"ColliderDetector2D OnCollisionExit2D {col.gameObject.name}");
        Remove(col.gameObject);
    }
    private void OnTriggerEnter2D(Collider2D col)
    {
        // Debug.Log($"ColliderDetector2D OnTriggerEnter2D {col.gameObject.name}");
        AddObject(col.gameObject);
    }
    private void OnTriggerExit2D(Collider2D col)
    {
        // Debug.Log($"ColliderDetector2D OnTriggerExit2D {col.gameObject.name}");
        Remove(col.gameObject);
    }

}
