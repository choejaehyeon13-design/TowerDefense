using UnityEngine;

public class WaypointGenerator : MonoBehaviour
{
    public GameObject waypointPrefab;

    void Start()
    {
        GenerateWaypoints();
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
            GameObject wp = Instantiate(waypointPrefab, points[i], Quaternion.identity);

            wp.name = "Waypoint_" + i;

            wp.transform.parent = this.transform;
        }
    }
}