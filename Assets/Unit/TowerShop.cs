using UnityEngine;

public class TowerShop : MonoBehaviour
{
    public static TowerShop Instance;

    public GameObject selectedTowerPrefab;
    public int selectedTowerCost;

    void Awake()
    {
        Instance = this;
    }

    public void SelectArcher(GameObject archerPrefab)
    {
        selectedTowerPrefab = archerPrefab;
        selectedTowerCost = 10;
        Debug.Log("아쳐타워 선택됨");
    }

    public void SelectMage(GameObject magePrefab)
    {
        selectedTowerPrefab = magePrefab;
        selectedTowerCost = 30;
        Debug.Log("마법사타워 선택됨");
    }

    public void SelectWarrior(GameObject warriorPrefab)
    {
        selectedTowerPrefab = warriorPrefab;
        selectedTowerCost = 40;
        Debug.Log("전사타워 선택됨");
    }
}