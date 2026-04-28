using UnityEngine;
using UnityEngine.Tilemaps;

public class EnemyPathPainter : MonoBehaviour
{
    [Header("타일을 그릴 Tilemap")]
    public Tilemap targetTilemap;

    [Header("적이 이동할 길 타일")]
    public TileBase enemyMoveTile;

    [Header("웨이포인트를 순서대로 넣기")]
    public Transform[] wayPoints;

    private void Start()
    {
        DrawPath();
    }

    public void DrawPath()
    {
        if (targetTilemap == null)
        {
            Debug.LogError("targetTilemap이 연결되지 않았습니다.");
            return;
        }

        if (enemyMoveTile == null)
        {
            Debug.LogError("enemyMoveTile이 연결되지 않았습니다.");
            return;
        }

        if (wayPoints == null || wayPoints.Length < 2)
        {
            Debug.LogError("wayPoints는 최소 2개 이상 필요합니다.");
            return;
        }

        targetTilemap.ClearAllTiles();

        for (int i = 0; i < wayPoints.Length - 1; i++)
        {
            if (wayPoints[i] == null || wayPoints[i + 1] == null)
            {
                Debug.LogError($"wayPoints[{i}] 또는 wayPoints[{i + 1}]가 비어 있습니다.");
                continue;
            }

            DrawLineBetweenPoints(wayPoints[i].position, wayPoints[i + 1].position);
        }
    }

    private void DrawLineBetweenPoints(Vector3 startWorldPos, Vector3 endWorldPos)
    {
        Vector3Int startCell = targetTilemap.WorldToCell(startWorldPos);
        Vector3Int endCell = targetTilemap.WorldToCell(endWorldPos);

        int dx = endCell.x - startCell.x;
        int dy = endCell.y - startCell.y;

        int stepX = dx == 0 ? 0 : (dx > 0 ? 1 : -1);
        int stepY = dy == 0 ? 0 : (dy > 0 ? 1 : -1);

        // 가로 / 세로 직선만 허용
        if (dx != 0 && dy != 0)
        {
            Debug.LogWarning($"대각선 경로는 지원하지 않습니다. 시작: {startCell}, 끝: {endCell}");
            return;
        }

        Vector3Int currentCell = startCell;
        targetTilemap.SetTile(currentCell, enemyMoveTile);

        while (currentCell != endCell)
        {
            currentCell.x += stepX;
            currentCell.y += stepY;
            targetTilemap.SetTile(currentCell, enemyMoveTile);
        }
    }
}