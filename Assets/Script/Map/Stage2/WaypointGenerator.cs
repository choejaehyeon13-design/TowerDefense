using UnityEngine;
using UnityEngine.Tilemaps;

[ExecuteAlways]
public class WaypointGenerator_Stage2 : MonoBehaviour
{
    [Header("웨이포인트 프리팹")]
    public GameObject waypointPrefab;

    [Header("경로 타일맵")]
    [Tooltip("갈색 길 타일이 찍혀 있는 Tilemap을 여기에 연결")]
    public Tilemap pathTilemap;

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
        // 여기 좌표는 "대략적인 경로 좌표"로 둔다.
        // 실제 waypoint 생성 위치는 pathTilemap 기준 타일 중앙으로 자동 보정된다.
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

            // 기본값은 기존 좌표
            Vector3 spawnPosition = positions[i];

            // pathTilemap이 연결되어 있으면
            // 입력 좌표가 속한 타일 셀을 찾고,
            // 그 셀의 정확한 중앙 좌표로 waypoint 위치를 보정한다.
            if (pathTilemap != null)
            {
                Vector3Int cellPosition = pathTilemap.WorldToCell(positions[i]);
                spawnPosition = pathTilemap.GetCellCenterWorld(cellPosition);
            }
            else
            {
                Debug.LogWarning("pathTilemap이 연결되지 않았습니다. waypoint가 원래 좌표에 생성됩니다.");
            }

            if (waypointPrefab != null)
            {
                obj = Instantiate(waypointPrefab, spawnPosition, Quaternion.identity, transform);
            }
            else
            {
                obj = new GameObject();
                obj.transform.SetParent(transform);
                obj.transform.position = spawnPosition;
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