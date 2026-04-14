using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    public static InventoryManager Instance;

    public ItemType currentItem; // 현재 보유한 아이템
    public bool hasItem = false; // 아이템 보유 여부 

    void Awake()
    {
        Instance = this;
    }

    void Update()
    {
        if (hasItem && Input.GetKeyDown(KeyCode.Space)) //아이템 보유 중 스페이스 바 - 아이템 사용
        {
            UseItem();
        }
    }

    public void AddItem(ItemType item) // 아이템 획득 함수
    {
        if (hasItem) {
            Debug.Log("이미 아이템 보유 중");
            return; // 이미 아이템이 있는 경우 추가 획득 방지
        }
        currentItem = item;
        Debug.Log("아이템 획득: " + item); 
        hasItem = true;

    }

    void UseItem()
    {
        if (GameManager.Instance == null) {
            Debug.Log("GameManager 연결 안됨");
            return;
        }
        if (GameManager.Instance.IsGameOver())
        {
            Debug.Log("게임 오버 상태");
            return;   
        } 

        switch (currentItem) // 인벤토리 아이템 사용
        {
            case ItemType.Dragon:
                //추후 구현
                break;

            case ItemType.PlayerHeal:
                GameManager.Instance.life += 2; //플레이어 체력 회복
                Debug.Log("플레이어 체력 회복"); //테스트용
                break;

            case ItemType.TeamBuff:
                // 추후 구현
                break;

            case ItemType.TimeSlow:
                // 추후 구현
                break;
        }

        hasItem = false; // 아이템 사용 후 삭제
    }
}