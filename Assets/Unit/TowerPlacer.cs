using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.EventSystems;
using System.Collections.Generic;

public class TowerPlacer : MonoBehaviour
{
    public static GameObject selectedTowerPrefab;
    public static int selectedTowerCost;

    public Tilemap buildTilemap;
    public GameObject highlightPrefab;

    private GameObject highlightObj;
    private GameObject previewTower;

    private Vector3Int currentCell;

    private HashSet<Vector3Int> occupiedCells =
        new HashSet<Vector3Int>();

    void Update()
    {
        // 타워 선택 안했으면 숨김
        if (selectedTowerPrefab == null)
        {
            HidePreview();
            return;
        }

        // 마우스 위치
        Vector3 mouseWorldPos =
            Camera.main.ScreenToWorldPoint(Input.mousePosition);

        mouseWorldPos.z = 0;

        // 현재 셀
        currentCell =
            buildTilemap.WorldToCell(mouseWorldPos);

        // 셀 중앙 위치
        Vector3 cellCenterPos =
            buildTilemap.GetCellCenterWorld(currentCell);

        // 설치 가능 여부
        bool canPlace = CanPlace(currentCell);

        // 미리보기 표시
        ShowPreview(cellCenterPos, canPlace);

        // 좌클릭 설치
        if (Input.GetMouseButtonDown(0))
        {
            PlaceTower(currentCell, cellCenterPos);
        }

        // 우클릭 취소
        if (Input.GetMouseButtonDown(1))
        {
            CancelPlacement();
        }
    }

    bool CanPlace(Vector3Int cellPos)
    {
        // 타일 없으면 설치 불가
        if (!buildTilemap.HasTile(cellPos))
        {
            return false;
        }

        // 이미 설치됨
        if (occupiedCells.Contains(cellPos))
        {
            return false;
        }

        return true;
    }

    void PlaceTower(Vector3Int cellPos, Vector3 placePos)
    {
        // 설치 불가
        if (!CanPlace(cellPos))
        {
            Debug.Log("설치 불가능한 타일");
            return;
        }

        // 골드 부족
        if (!Gold.Instance.UseGold(selectedTowerCost))
        {
            Debug.Log("골드 부족");
            return;
        }

        // 타워 생성
        Instantiate(
            selectedTowerPrefab,
            placePos,
            Quaternion.identity
        );

        // 위치 저장
        occupiedCells.Add(cellPos);

        Debug.Log("타워 설치 완료");

        // 선택 해제
        selectedTowerPrefab = null;

        HidePreview();
    }

    void CancelPlacement()
    {
        selectedTowerPrefab = null;

        HidePreview();

        Debug.Log("설치 취소");
    }

    void ShowPreview(Vector3 pos, bool canPlace)
    {
        // 미리보기 생성
        if (previewTower == null)
        {
            previewTower = Instantiate(
                selectedTowerPrefab,
                pos,
                Quaternion.identity
            );

            // 스크립트 비활성화
            MonoBehaviour[] scripts =
                previewTower.GetComponents<MonoBehaviour>();

            foreach (MonoBehaviour script in scripts)
            {
                script.enabled = false;
            }
        }

        // 위치 이동
        previewTower.transform.position = pos;

        // 색상 변경
        SpriteRenderer towerSR =
            previewTower.GetComponent<SpriteRenderer>();

        if (towerSR != null)
        {
            if (canPlace)
            {
                towerSR.color =
                    new Color(1, 1, 1, 0.8f);
            }
            else
            {
                towerSR.color =
                    new Color(1, 0.4f, 0.4f, 0.8f);
            }
        }

        // 하이라이트 생성
        if (highlightPrefab != null &&
            highlightObj == null)
        {
            highlightObj = Instantiate(
                highlightPrefab,
                pos,
                Quaternion.identity
            );
        }

        // 하이라이트 표시
        if (highlightObj != null)
        {
            highlightObj.SetActive(true);

            highlightObj.transform.position = pos;

            SpriteRenderer sr =
                highlightObj.GetComponent<SpriteRenderer>();

            if (sr != null)
            {
                if (canPlace)
                {
                    sr.color =
                        new Color(0, 1, 0, 0.4f);
                }
                else
                {
                    sr.color =
                        new Color(1, 0, 0, 0.4f);
                }
            }
        }
    }

    void HidePreview()
    {
        if (previewTower != null)
        {
            Destroy(previewTower);

            previewTower = null;
        }

        if (highlightObj != null)
        {
            highlightObj.SetActive(false);
        }
    }
}