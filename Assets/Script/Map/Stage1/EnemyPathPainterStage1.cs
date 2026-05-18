using UnityEngine;
using UnityEngine.Tilemaps;

[ExecuteAlways]
public class EnemyPathPainter_Stage1 : MonoBehaviour
{
    [Header("타일을 그릴 Tilemap")]
    public Tilemap targetTilemap;

    [Header("적이 이동할 길 타일")]
    public TileBase enemyMoveTile;

    [Header("웨이포인트 생성기")]
    public WaypointGenerator_Stage1 waypointGenerator;

    [Header("에디터에서 자동으로 길 그리기")]
    public bool autoPaintInEditor = true;

    private void OnEnable()
    {
        PaintPath();
    }

#if UNITY_EDITOR
    private void OnValidate()
    {
        if (!autoPaintInEditor) return;

        UnityEditor.EditorApplication.delayCall += () =>
        {
            if (this == null) return;
            PaintPath();
        };
    }
#endif

    public void PaintPath()
    {
        if (targetTilemap == null)
        {
            Debug.LogWarning("EnemyPathPainter: targetTilemap이 비어 있습니다.");
            return;
        }

        if (enemyMoveTile == null)
        {
            Debug.LogWarning("EnemyPathPainter: enemyMoveTile이 비어 있습니다.");
            return;
        }

        if (waypointGenerator == null)
        {
            Debug.LogWarning("EnemyPathPainter: waypointGenerator가 비어 있습니다.");
            return;
        }

        // 핵심: 먼저 웨이포인트를 확실히 생성
        if (waypointGenerator.leftWayPoints == null || waypointGenerator.leftWayPoints.Length < 2)
        {
            waypointGenerator.GenerateWaypoints();
        }

        targetTilemap.ClearAllTiles();

        // Stage1은 왼쪽 경로만 그림
        DrawPath(waypointGenerator.leftWayPoints);

        targetTilemap.RefreshAllTiles();

        Debug.Log("Stage1 적 이동 경로 타일 생성 완료");
    }

    private void DrawPath(Transform[] waypoints)
    {
        if (waypoints == null || waypoints.Length < 2)
        {
            Debug.LogWarning("DrawPath: 웨이포인트가 부족합니다.");
            return;
        }

        for (int i = 0; i < waypoints.Length - 1; i++)
        {
            if (waypoints[i] == null || waypoints[i + 1] == null)
                continue;

            PaintLine(waypoints[i].position, waypoints[i + 1].position);
        }
    }

    private void PaintLine(Vector3 startWorldPos, Vector3 endWorldPos)
    {
        Vector3Int startCell = targetTilemap.WorldToCell(startWorldPos);
        Vector3Int endCell = targetTilemap.WorldToCell(endWorldPos);

        int dx = endCell.x - startCell.x;
        int dy = endCell.y - startCell.y;

        int stepX = dx == 0 ? 0 : dx > 0 ? 1 : -1;
        int stepY = dy == 0 ? 0 : dy > 0 ? 1 : -1;

        if (dx != 0 && dy != 0)
        {
            Debug.LogWarning($"대각선 경로는 지원하지 않습니다. 시작: {startCell}, 끝: {endCell}");
            return;
        }

        Vector3Int currentCell = startCell;
        targetTilemap.SetTile(currentCell, enemyMoveTile);

        while (currentCell != endCell)
        {
            currentCell = new Vector3Int(
                currentCell.x + stepX,
                currentCell.y + stepY,
                0
            );

            targetTilemap.SetTile(currentCell, enemyMoveTile);
        }
    }
}