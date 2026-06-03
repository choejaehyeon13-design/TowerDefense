using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    public static InventoryManager Instance;

    public ItemSlot slot; 

    void Awake()
    {
        Instance = this;
        
    }
    void Start()
    {
        if (GameManager.Instance == null){
            Debug.Log("게임 매니저 연결되지 않음");
            return;
        }
    }

    public void setInven(ItemType item) //아이템 획득시 currentItem 갱신
    {
        slot.SetItem(item);
    }

    
}