using UnityEngine;

public class TowerShop : MonoBehaviour
{
    public GameObject selectedTowerPrefab;
    public int selectedTowerCost;

    public void SelectTower()
    {
        Debug.Log("버튼 눌림");

        TowerPlacer.selectedTowerPrefab = selectedTowerPrefab;
        TowerPlacer.selectedTowerCost = selectedTowerCost;

        Debug.Log(selectedTowerPrefab.name + " 선택됨 / 가격: " + selectedTowerCost);
    }
}