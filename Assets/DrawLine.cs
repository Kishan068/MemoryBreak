using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class DrawLine : MonoBehaviour
{
    public Transform startPoint; // Object where the line starts
    public Transform endPoint;   // Object where the line ends
    private LineRenderer lineRenderer;

    void Start()
    {
        // Get the LineRenderer component attached to this GameObject
        lineRenderer = GetComponent<LineRenderer>();

        // Set some basic properties for the line
        lineRenderer.positionCount = 2; // LineRenderer needs 2 points (start and end)
        lineRenderer.startWidth = 0.1f; // Width of the line at the start
        lineRenderer.endWidth = 0.1f;   // Width of the line at the end

        
    }

    void Update()
    {
        // Check if both startPoint and endPoint are set
        if (startPoint != null && endPoint != null)
        {
            // Set the positions for the start and end points of the line
            //add y offset to the end point
            endPoint.position = new Vector3(endPoint.position.x, endPoint.position.y + 1f, endPoint.position.z);
            lineRenderer.SetPosition(0, startPoint.position); // Start of the line
            lineRenderer.SetPosition(1, endPoint.position);   // End of the line
        }
    }
}