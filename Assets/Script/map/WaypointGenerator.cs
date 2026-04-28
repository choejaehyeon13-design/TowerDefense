using UnityEngine;

public class WaypointGenerator : MonoBehaviour
{
    [Header("웨이포인트 프리팹")]
    public GameObject waypointPrefab;

    [Header("자동 생성된 왼쪽 경로")]
    public Transform[] leftWayPoints;

    [Header("자동 생성된 오른쪽 경로")]
    public Transform[] rightWayPoints;

    [Header("자동 생성된 하단 경로")]
    public Transform[] bottomWayPoints;

    private void Awake()
    {
        GenerateWaypoints();
    }

    public void GenerateWaypoints()
    {
        if (waypointPrefab == null)
        {
            Debug.LogError("Waypoint Prefab이 연결되지 않았습니다.");
            return;
        }

        ClearOldWaypoints();

        Vector3[] leftPath =
        {
            new Vector3(-8, 0, 0),
            new Vector3(-6, 0, 0),
            new Vector3(-6, 3, 0),
            new Vector3(-3, 3, 0),
            new Vector3(-3, 0, 0),
            new Vector3(0, 0, 0),
        };

        Vector3[] rightPath =
        {
            new Vector3(8, 0, 0),
            new Vector3(6, 0, 0),
            new Vector3(6, -3, 0),
            new Vector3(3, -3, 0),
            new Vector3(3, 0, 0),
            new Vector3(0, 0, 0),
        };

        Vector3[] bottomPath =
        {
            new Vector3(0, -5, 0),
            new Vector3(0, -3, 0),
            new Vector3(-2, -3, 0),
            new Vector3(-2, 2, 0),
            new Vector3(2, 2, 0),
            new Vector3(2, 0, 0),
            new Vector3(0, 0, 0),
        };

        leftWayPoints = CreateWaypoints(leftPath, "LeftWayPoint");
        rightWayPoints = CreateWaypoints(rightPath, "RightWayPoint");
        bottomWayPoints = CreateWaypoints(bottomPath, "BottomWayPoint");

        Debug.Log("웨이포인트 생성 완료: "
            + leftWayPoints.Length + ", "
            + rightWayPoints.Length + ", "
            + bottomWayPoints.Length);
    }

    private Transform[] CreateWaypoints(Vector3[] path, string prefix)
    {
        Transform[] result = new Transform[path.Length];

        for (int i = 0; i < path.Length; i++)
        {
            GameObject point = Instantiate(
                waypointPrefab,
                path[i],
                Quaternion.identity,
                transform
            );

            point.name = prefix + "_" + i;
            result[i] = point.transform;
        }

        return result;
    }

    private void ClearOldWaypoints()
    {
        for (int i = transform.childCount - 1; i >= 0; i--)
        {
            Destroy(transform.GetChild(i).gameObject);
        }
    }
}