using System.Collections.Generic;
using UnityEngine;

public class ObjectColliderDetector : MonoBehaviour
{

    [SerializeField]
    protected float timeLastSort = 0;
    protected List<GameObject> objectList = new();
    public List<GameObject> Objects
    {
        get
        {
            switch (Time.time - timeLastSort)
            {
                case > 0.2f:
                    SortListByDistance();
                    break;
                default:
                    break;
            }
            return objectList;
        }
    }

    private void OnCollisionEnter(Collision col)
    {
        // Debug.Log($"ColliderDetector OnCollisionEnter {col.gameObject.name}");
        AddObject(col.gameObject);
    }
    private void OnCollisionExit(Collision col)
    {
        // Debug.Log($"ColliderDetector OnCollisionExit {col.gameObject.name}");
        Remove(col.gameObject);
    }
    private void OnTriggerEnter(Collider col)
    {
        // Debug.Log($"ColliderDetector OnTriggerEnter {col.gameObject.name}");
        AddObject(col.gameObject);
    }
    private void OnTriggerExit(Collider col)
    {
        // Debug.Log($"ColliderDetector OnTriggerExit {col.gameObject.name}");
        Remove(col.gameObject);
    }

    protected void AddObject(GameObject gameObject)
    {
        objectList.Add(gameObject);
        SortListByDistance();
    }
    protected void Remove(GameObject gameObject)
    {
        objectList.Remove(gameObject);
    }

    protected void SortListByDistance()
    {
        objectList.Sort(SortGameObjectsByDistance);

        timeLastSort = Time.time;
    }
    protected int SortGameObjectsByDistance(GameObject comp1, GameObject comp2)
    {
        float distanceComp1 = Vector3.Distance(transform.position, comp1.transform.position);
        float distanceComp2 = Vector3.Distance(transform.position, comp2.transform.position);
        float distanceDiff = distanceComp1 - distanceComp2;

        // Debug.Log($"{comp1.name} {distanceComp1} / {comp2.name} {distanceComp2} / {distanceDiff} {Mathf.CeilToInt(distanceDiff)}");

        return Mathf.CeilToInt(distanceDiff);
    }

}
