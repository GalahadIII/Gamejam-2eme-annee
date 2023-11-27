using System.Collections.Generic;
using UnityEngine;

public class LineDrawer
{
    private GameObject lineObj;
    private List<LineRenderer> lineRenderers;
    private float lineSize;
    private Material material;

    public LineDrawer(float lineSize = 0.01f)
    {
        Init(lineSize);
    }

    private void Init(float lineSize = 0.01f)
    {
        Reset();
        if (lineObj == null)
        {
            lineObj = new GameObject("LineDrawer");
        }
        lineRenderers = new List<LineRenderer>();
        this.lineSize = lineSize;
        material = new Material(Shader.Find("Hidden/Internal-Colored"));
    }

    public void Reset()
    {
        if (lineObj == null)
        {
            return;
        }
        if (lineRenderers == null)
        {
            return;
        }
        foreach (LineRenderer lineRenderer in lineRenderers)
        {
            UnityEngine.Object.Destroy(lineRenderer);
        }
    }

    //Draws lines through the provided vertices
    public void DrawLineInGameView(Vector3 start, Vector3 end, Color color)
    {
        LineRenderer lineRenderer = lineObj.AddComponent<LineRenderer>();

        lineRenderer.startColor = color;
        lineRenderer.endColor = color;

        lineRenderer.startWidth = lineSize;
        lineRenderer.endWidth = lineSize;

        lineRenderer.positionCount = 2;

        lineRenderer.material = material;

        lineRenderer.SetPosition(0, start);
        lineRenderer.SetPosition(1, end);

        lineRenderers.Add(lineRenderer);
    }

    public void Destroy()
    {
        UnityEngine.Object.Destroy(lineObj);
    }
}
