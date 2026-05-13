using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TowerPlacementManager : MonoBehaviour
{
    [Header("설치 가능한 타일맵")]
    public Tilemap buildTilemap;

    [Header("설치할 타워 프리팹")]
    public GameObject towerPrefab;

    [Header("게임 화면 제한")]
    public float minX = -16f;
    public float maxX = 16f;
    public float minY = -9f;
    public float maxY = 9f;

    [Header("타워 설치가 가능한 타일")]
    public TileBase DeployableTile;

    private HashSet<Vector3Int> occupiedCells = new HashSet<Vector3Int>();

    private bool isPlacing = false;

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            DeployTower();
        }

        if (Input.GetMouseButtonDown(1))
        {
            CancelDeployment();
        }
    }

    public void SelectTower(GameObject selectedTowerPrefab)
    {
        towerPrefab = selectedTowerPrefab;
        isPlacing = true;

        Debug.Log("타워 선택됨");
    }

    void DeployTower()
    {
        if (!isPlacing || towerPrefab == null)
        {
            Debug.Log("선택된 타워가 없습니다.");
            return;
        }

        if (buildTilemap == null)
        {
            Debug.Log("buildTilemap이 연결되지 않았습니다.");
            return;
        }

        if (DeployableTile == null)
        {
            Debug.Log("DeployableTile이 연결되지 않았습니다.");
            return;
        }

        Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mouseWorldPos.z = 0f;

        if (
            mouseWorldPos.x < minX ||
            mouseWorldPos.x > maxX ||
            mouseWorldPos.y < minY ||
            mouseWorldPos.y > maxY
        )
        {
            Debug.Log("게임 화면 밖이라 설치 불가");
            return;
        }

        Vector3Int cellPos = buildTilemap.WorldToCell(mouseWorldPos);

        TileBase currentTile = buildTilemap.GetTile(cellPos);

        if (currentTile != DeployableTile)
        {
            Debug.Log("타워 설치 전용 타일이 아니라서 설치 불가");
            return;
        }

        if (occupiedCells.Contains(cellPos))
        {
            Debug.Log("이미 타워가 설치된 위치");
            return;
        }

        Vector3 spawnPos = buildTilemap.GetCellCenterWorld(cellPos);

        Instantiate(
            towerPrefab,
            spawnPos,
            Quaternion.identity
        );

        occupiedCells.Add(cellPos);

        Debug.Log("타워 설치 완료");

        CancelDeployment();
    }

    void CancelDeployment()
    {
        towerPrefab = null;
        isPlacing = false;

        Debug.Log("타워 설치 취소");
    }
}