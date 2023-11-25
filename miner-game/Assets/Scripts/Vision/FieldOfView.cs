using UnityEngine;

public class FieldOfView : MonoBehaviour
{
    [SerializeField] private float fov = 360f;
    [SerializeField] private float viewDistance = 5f;
    [SerializeField] private int rayCount = 20;
    [SerializeField] private LayerMask layerMask;

    private Mesh _mesh;

    private void Start()
    {
        _mesh = new Mesh();
        GetComponent<MeshFilter>().mesh = _mesh;
    }
    private void Update()
    {
        // Debug.LogWarning("UPDATE");
        float angle = 0f;
        float angleIncrease = fov / rayCount;

        Vector3[] vertices = new Vector3[rayCount + 1 + 1];
        Vector2[] uv = new Vector2[vertices.Length];
        int[] triangles = new int[rayCount * 3];

        // start at local position
        vertices[0] = Vector3.zero;

        int vertexIndex = 1;
        int triangleIndex = 0;
        for (int i = 0; i <= rayCount; i++)
        {
            Vector3 angleVector = GetVectorFromAngle(angle);

            Vector3 vertex = angleVector * viewDistance;

            RaycastHit2D raycastHit2D = Physics2D.Raycast(transform.position, angleVector, viewDistance, layerMask);

            // Debug.Log($"FOW RAY {i}/{rayCount} {transform.position} / {angle} {angleVector * viewDistance} {raycastHit2D.point}");

            if (raycastHit2D.collider != null)
            {
                vertex = (Vector3)raycastHit2D.point - transform.position;
            }

            // Debug.DrawRay(transform.position, vertex);

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

}
