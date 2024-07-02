using System;
using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
[RequireComponent(typeof(BoxCollider))]
public class BoxColliderHighlighter : MonoBehaviour
{
    private LineRenderer lineRenderer;
    private BoxCollider boxCollider;

    void Awake()
    {
        lineRenderer = GetComponent<LineRenderer>();
        boxCollider = GetComponent<BoxCollider>();

        // Configure LineRenderer settings
        lineRenderer.positionCount = 5;  // 4 corners plus 1 to close the loop
        lineRenderer.loop = true;
        lineRenderer.useWorldSpace = false;  // Local space allows it to move with the object
    }

    void Start()
    {
        HideHighlighter();
    }

    public void ShowHighlighter()
    {
        lineRenderer.positionCount = 5;

        Vector3[] points = new Vector3[5];

        // Get BoxCollider dimensions
        Vector3 center = boxCollider.center;
        Vector3 size = boxCollider.size;

        // Calculate half size
        Vector3 halfSize = size / 2;

        // Define the positions for the bottom square
        points[0] = new Vector3(center.x - halfSize.x, center.y - halfSize.y, center.z - halfSize.z); // Bottom-left
        points[1] = new Vector3(center.x + halfSize.x, center.y - halfSize.y, center.z - halfSize.z); // Bottom-right
        points[2] = new Vector3(center.x + halfSize.x, center.y - halfSize.y, center.z + halfSize.z); // Top-right
        points[3] = new Vector3(center.x - halfSize.x, center.y - halfSize.y, center.z + halfSize.z); // Top-left
        points[4] = points[0]; // Close the loop

        // Set points to LineRenderer
        lineRenderer.SetPositions(points);
    }

    public void HideHighlighter()
    {
        lineRenderer.positionCount = 0;
    }
}
