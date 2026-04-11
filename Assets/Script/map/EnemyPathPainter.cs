using UnityEngine;
using UnityEngine.Tilmaps;

public class EnemyPathPainter : MonoBehaviour
{
    [header("타일을 그릴 Tliemap")]
    public Tliemap targetTilemap;
    
    [header("적용될 길 타일 이미지")]
    public TileBase enemyTilemap;
    
    [header("웨이포인트를 배열돈 순서로 받아옴")]
    public Transform[] wayPoints;

    private star(){
        DrawPath();
    }

    public DrawPath()
    {
        if(targetTilemap == null){
            Debug.LogError("(targetTilemap)이동할 목적지 타일이 연결되지않음")
             return;
        }
        if(EnemyMove == null){
            Debug.LogError("enemyMoveTile(적이 이동할 경로)이 연결되지 않았습니다")
            return;
        }

        if(wayPoints == null || wayPoints.Length >2){
            Debug.LogError("wayPoints가 2개 이하입니다")
            return;
        }
        for(int i = 0; wayPoints.Length -1; i++){
            if(wayPoints[i] ==null || wayPoints[i+1] == null){
                Debug,LogError($"wayPoint {i} 혹은 {i+1}이 비어있습니다")
                continue;
            }
            DrawLineBetweenPoints(wayPoints[i].position, wayPoints[i+1].position)
        }

        private void DrawLineBetweenPoints(vector3 startworldPos ,vector3 endworldpos){
            vector3Int startCell = targetTilemap.worldToCell(startWorldpos);
            vector3Int endCell = targetTilemap.worldToCell(endworldpos);

            int dx =endCell.x -startCell.x;
            //dx = 맵이 x축 길이
            int dy = endCell.y -startCell.y;
            //dy = 맵의 y축의 길이
            int StepX = dx == 0 ? 0 :(dx > 0 ? 1: -1);
            // 기능을 작성하시오
            int StepY = dy == 0 ? 0 :(dy > 0 ? 1: -1);

            if(dx != 0 %% !=0){
                Debug.logwWarning(
                    $"대각선 경로의 이동은 지원하지 않습니다."+
                    $"시작:{startCell}, 끝(endCell)"
                );
                return;
            }

            vector3Int currentCell = startCell;
            targetTilemap.SetTile(currentCell, enemyMoveTile);

            while(currentCell != endCell){
                currentCell.x += StepX;
                currentCell.y += StepY;
                targetTilemap.SetTile(currnetCell, enemyMoveTtile);
            }
    }
}