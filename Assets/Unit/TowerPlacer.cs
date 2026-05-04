using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.EventSystems;
using System.Collections.Generic;

public class TowerPlacer : MonoBehaviour
{
    public Tilemap buildTilemap;
    public GameObject highlightPrefab;

    private GameObject highlightObj;
    private GameObject previewTower;

    private Vector3Int currentCell;
    private HashSet<Vector3Int> occupiedCells = new HashSet<Vector3Int>();

    void Update()
    {
        if (EventSystem.current != null && EventSystem.current.IsPointerOverGameObject())
            return;

        if (TowerShop.Instance.selectedTowerPrefab == null)
        {
            HidePreview();
            return;
        }

        Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mouseWorldPos.z = 0;

        currentCell = buildTilemap.WorldToCell(mouseWorldPos);
        Vector3 cellCenterPos = buildTilemap.GetCellCenterWorld(currentCell);

        bool canPlace = CanPlace(currentCell);

        ShowPreview(cellCenterPos, canPlace);

        if (Input.GetMouseButtonDown(0))
        {
            PlaceTower(currentCell, cellCenterPos);
        }

        if (Input.GetMouseButtonDown(1))
        {
            CancelPlacement();
        }
    }

    bool CanPlace(Vector3Int cellPos)
    {
        if (!buildTilemap.HasTile(cellPos))
            return false;

        if (occupiedCells.Contains(cellPos))
            return false;

        return true;
    }

    void PlaceTower(Vector3Int cellPos, Vector3 placePos)
    {
        if (!CanPlace(cellPos))
        {
            Debug.Log("설치 가능한 타일이 아닙니다.");
            return;
        }

        if (!Gold.Instance.UseGold(TowerShop.Instance.selectedTowerCost))
            return;

        Instantiate(
            TowerShop.Instance.selectedTowerPrefab,
            placePos,
            Quaternion.identity
        );

        occupiedCells.Add(cellPos);

        Debug.Log("타워 설치 완료");
    }

    void CancelPlacement()
    {
        TowerShop.Instance.selectedTowerPrefab = null;
        HidePreview();
    }

    void ShowPreview(Vector3 pos, bool canPlace)
    {
        if (previewTower == null)
        {
            previewTower = Instantiate(
                TowerShop.Instance.selectedTowerPrefab,
                pos,
                Quaternion.identity
            );

            MonoBehaviour[] scripts = previewTower.GetComponents<MonoBehaviour>();
            foreach (MonoBehaviour script in scripts)
                script.enabled = false;
        }

        previewTower.transform.position = pos;

        SpriteRenderer towerSR = previewTower.GetComponent<SpriteRenderer>();
        if (towerSR != null)
        {
            towerSR.color = canPlace ?
                new Color(1,1,1,0.8f) :
                new Color(1,0.4f,0.4f,0.8f);
        }

        if (highlightPrefab != null && highlightObj == null)
        {
            highlightObj = Instantiate(highlightPrefab, pos, Quaternion.identity);
        }

        if (highlightObj != null)
        {
            highlightObj.SetActive(true);
            highlightObj.transform.position = pos;

            SpriteRenderer sr = highlightObj.GetComponent<SpriteRenderer>();
            if (sr != null)
            {
                sr.color = canPlace ?
                    new Color(0,1,0,0.4f) :
                    new Color(1,0,0,0.4f);
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