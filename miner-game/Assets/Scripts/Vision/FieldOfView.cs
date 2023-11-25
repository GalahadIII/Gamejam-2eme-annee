using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class FieldOfView : MonoBehaviour
{
    [SerializeField] private float fov = 360f;
    [SerializeField] private float viewDistance = 5f;
    [SerializeField] private int rayCount = 20;
    [SerializeField] private LayerMask layerMask;

    private Mesh _mesh;
    private Vector3 _origin;

    // Start is called before the first frame update
    private void Start()
    {
        _mesh = new Mesh();
        _origin = Vector3.zero;
        GetComponent<MeshFilter>().mesh = _mesh;
    }

    // Update is called once per frame
    private void Update()
    {
        _origin = transform.position;

        float angle = 0f;
        float angleIncrease = fov / rayCount;

        Vector3[] vertices = new Vector3[rayCount + 1 + 1];
        Vector2[] uv = new Vector2[vertices.Length];
        int[] triangles = new int[rayCount * 3];



        vertices[0] = _origin;

        int vertexIndex = 1;
        int triangleIndex = 0;
        for (int i = 0; i <= rayCount; i++)
        {
            Vector3 vertex;
            RaycastHit2D raycastHit2D = Physics2D.Raycast(_origin, GetVectorFromAngle(angle), viewDistance, layerMask);
            if (raycastHit2D.collider == null)
            {
                // No hit
                vertex = _origin + GetVectorFromAngle(angle) * viewDistance;
            }
            else
            {
                // hit wall
                vertex = raycastHit2D.point;
            }
            vertices[vertexIndex] = vertex;

            if (i > 0)
            {
                triangles[triangleIndex + 0] = 0;
                triangles[triangleIndex + 1] = vertexIndex - 1;
                triangles[triangleIndex + 2] = vertexIndex;

                triangleIndex += 3;
            }

            vertexIndex++;
            angle -= angleIncrease;
        }

        _mesh.vertices = vertices;
        _mesh.uv = uv;
        _mesh.triangles = triangles;

        _mesh.RecalculateBounds();
    }

    private static Vector3 GetVectorFromAngle(float angle)
    {
        // angle = 0 -> 360
        float angleRad = angle * (Mathf.PI / 180f);
        return new Vector3(Mathf.Cos(angleRad), Mathf.Sin(angleRad));
    }

    public void setOrigin(Vector3 origin)
    {
        this._origin = origin;
    }
}
