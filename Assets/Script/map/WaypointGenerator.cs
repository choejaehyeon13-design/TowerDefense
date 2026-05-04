using UnityEngine;

public class WaypointGenerator : MonoBehaviour
{
    public GameObject waypointPrefab;

    void Start()
    {
        ClearChildren();
        GenerateWaypoints();
        ApplyWaypointsToPathPainter();
    }

    void ClearChildren()
    {
        foreach (Transform child in transform)
        {
            Destroy(child.gameObject);
        }
    }

    void GenerateWaypoints()
    {
        Vector3[] points = new Vector3[]
        {
            new Vector3(-7f, -3.5f, 0),
            new Vector3(-4f, -3.5f, 0),

            new Vector3(-4f,  2.5f, 0),
            new Vector3(-1f,  2.5f, 0),

            new Vector3(-1f, -2.5f, 0),
            new Vector3( 2f, -2.5f, 0),

            new Vector3( 2f,  2.5f, 0),
            new Vector3( 5f,  2.5f, 0),

            new Vector3( 5f, -3.5f, 0),
            new Vector3( 7f, -3.5f, 0),
        };

        for (int i = 0; i < points.Length; i++)
        {
            GameObject wayPoint = Instantiate(waypointPrefab, points[i], Quaternion.identity);
            wayPoint.name = "Waypoint_" + i;
            wayPoint.transform.SetParent(transform);
        }
    }

    void ApplyWaypointsToPathPainter()
    {
        EnemyPathPainter pathPainter = GetComponent<EnemyPathPainter>();

        if (pathPainter != null)
        {
            Transform[] children = GetComponentsInChildren<Transform>();

            pathPainter.wayPoints = new Transform[children.Length - 1];

            for (int i = 1; i < children.Length; i++)
            {
                pathPainter.wayPoints[i - 1] = children[i];
            }

            pathPainter.DrawPath();
        }
    }
}