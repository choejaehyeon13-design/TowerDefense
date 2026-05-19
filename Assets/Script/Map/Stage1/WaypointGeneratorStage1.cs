using UnityEngine;

public class WaypointGenerator_Stage1 : MonoBehaviour
{
    [Header("웨이포인트 프리팹")]
    public GameObject waypointPrefab;

    [Header("자동 생성된 웨이포인트")]
    public Transform[] wayPoints;

    public void GenerateWaypoints()
    {
        ClearWaypoints();

        Vector3[] path =
        {
            new Vector3(-15, 0, 0),
            new Vector3(-10, 0, 0),
            new Vector3(-10, 4, 0),
            new Vector3(-6, 4, 0),
            new Vector3(-6, -3, 0),
            new Vector3(0, -3, 0),
            new Vector3(0, 4, 0),
            new Vector3(6, 4, 0),
            new Vector3(6, 0, 0),
            new Vector3(15, 0, 0)
        };
        wayPoints = new Transform[path.Length];

        for (int i = 0; i < path.Length; i++)
        {
            GameObject point = Instantiate(
                waypointPrefab,
                path[i],
                Quaternion.identity,
                transform
            );
            point.name = "WayPoint" + i;

            wayPoints[i] = point.transform;
        }

       
    }

    private void ClearWaypoints()
    {
        for (int i = transform.childCount - 1; i >= 0; i--)
        {
            DestroyImmediate(transform.GetChild(i).gameObject);
        }
    }
}