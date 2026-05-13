using UnityEngine;

[ExecuteAlways]
public class WaypointGenerator_Stage1 : MonoBehaviour
{
    [Header("웨이포인트 프리팹")]
    public GameObject waypointPrefab;

    [Header("Stage1 경로")]
    public Transform[] leftWayPoints;

    [Header("Stage1에서는 사용하지 않음")]
    public Transform[] rightWayPoints;

    [Header("에디터에서 자동 생성")]
    public bool autoGenerateInEditor = true;

    private void Awake()
    {
        if (Application.isPlaying)
        {
            if (leftWayPoints == null || leftWayPoints.Length == 0)
            {
                GenerateWaypoints();
            }
        }
    }

#if UNITY_EDITOR
    private void OnValidate()
    {
        if (!autoGenerateInEditor) return;
        if (Application.isPlaying) return;

        UnityEditor.EditorApplication.delayCall += () =>
        {
            if (this == null) return;
            GenerateWaypoints();
        };
    }
#endif

    public void GenerateWaypoints()
    {
        ClearOldWaypoints();

        // Stage1: 왼쪽에서 출발해서 오른쪽 끝으로 이동
        Vector3[] stage1Positions =
        {
            new Vector3(-15, 0, 0),
            new Vector3(-10, 0, 0),
            new Vector3(-10, 5, 0),
            new Vector3(-8, 5, 0),
            new Vector3(-8, -4, 0),
            new Vector3(-5, -4, 0),
            new Vector3(-5, 5, 0),
            new Vector3(2, 5, 0),
            new Vector3(2, -4, 0),
            new Vector3(5, -4, 0),
            new Vector3(5, 5, 0),
            new Vector3(9, 5, 0),
            new Vector3(9, 0, 0),
            new Vector3(15, 0, 0)
        };

        leftWayPoints = CreateWaypoints(stage1Positions, "Stage1WayPoint");

        // Stage1에서는 오른쪽 경로 없음
        rightWayPoints = new Transform[0];
    }

    private Transform[] CreateWaypoints(Vector3[] positions, string prefix)
    {
        Transform[] result = new Transform[positions.Length];

        for (int i = 0; i < positions.Length; i++)
        {
            GameObject obj;

            if (waypointPrefab != null)
            {
                obj = Instantiate(waypointPrefab, positions[i], Quaternion.identity, transform);
            }
            else
            {
                obj = new GameObject();
                obj.transform.SetParent(transform);
                obj.transform.position = positions[i];
            }

            obj.name = $"{prefix}_{i}";
            result[i] = obj.transform;
        }

        return result;
    }

    private void ClearOldWaypoints()
    {
        for (int i = transform.childCount - 1; i >= 0; i--)
        {
            Transform child = transform.GetChild(i);

            if (Application.isPlaying)
                Destroy(child.gameObject);
            else
                DestroyImmediate(child.gameObject);
        }
    }
}