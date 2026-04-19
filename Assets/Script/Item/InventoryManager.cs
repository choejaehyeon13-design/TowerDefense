using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    public static InventoryManager Instance;

    public ItemType currentItem = ItemType.None;
    public ItemSlot slot; 
    public MageTower cooldown;
    public float dragonRadius = 3f;
    public int dragonDamage = 3;
    public float TimeSlowLast = 3f;

    void Awake()
    {
        Instance = this;
    }

    void Update()
    {
        if (currentItem != ItemType.None && Input.GetKeyDown(KeyCode.Space))
        {
            UseItem();
        }
        
    }

    public void AddItem(ItemType item)
    {
        currentItem = item;
        Debug.Log("아이템 획득: " + item);

        slot.SetItem(item);
    }

    void UseItem()
    {
        EnemyHealth enemyHealth = GetComponent<EnemyHealth>();
        if (GameManager.Instance == null){
            Debug.Log("게임 매니저 연결되지 않음");
            return;
        }
        if (GameManager.Instance.IsGameOver()) {
            Debug.Log("게임 오버 상태");
            return;
        }
        switch (currentItem)
        {
            case ItemType.Dragon:
                Debug.Log("드래곤 사용");
                UseDragon();
                break;
            case ItemType.PlayerHeal:
                GameManager.Instance.life += 2;
                Debug.Log("체력 회복");
                break;

            case ItemType.TeamBuff:
                Debug.Log("버프 사용");
                break;
            case ItemType.TimeSlow:
                Debug.Log("시간 아이템 사용");
                StartCoroutine(TimeSlow());
                break;
        }

        currentItem = ItemType.None;
        slot.SetItem(ItemType.None);
    }
    void UseDragon()
    {
        
        Vector2 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        
        Collider2D[] hits = Physics2D.OverlapCircleAll(pos, dragonRadius);

        foreach (var hit in hits)
        {
            if (hit.CompareTag("Enemy"))
            {
                EnemyHealth enemy = hit.GetComponent<EnemyHealth>();
                
                if (enemy != null)
                {
                    enemy.TakeDamage(dragonDamage);
                }
            }
        }
    }
    IEnumerator TimeSlow()
    {
        EnemyMove[] enemies = FindObjectsOfType<EnemyMove>();

        foreach (var move in enemies)
        {
            move.speed -= 1f;
            yield return new WaitForSeconds(TimeSlowLast);
            move.speed += 1f;
        }
    }
}