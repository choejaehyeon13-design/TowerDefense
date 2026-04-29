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
    public float TeamBuffLast = 2f;

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

    void Update() //스페이스로 아이템 사용
    {
        if (currentItem != ItemType.None && Input.GetKeyDown(KeyCode.Space))
        {
            UseItem();
        }
        
    }

    public void AddItem(ItemType item) //아이템 획득시 currentItem 갱신
    {
        currentItem = item;
        Debug.Log("아이템 획득: " + item);

        slot.SetItem(item);
    }

    void UseItem() //아이템 사용
    {
        EnemyHealth enemyHealth = GetComponent<EnemyHealth>();
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
                StartCoroutine(TeamBuff());
                break;
            case ItemType.TimeSlow:
                Debug.Log("시간 아이템 사용");
                StartCoroutine(TimeSlow());
                break;
        }

        currentItem = ItemType.None;
        slot.SetItem(ItemType.None);
    }
    void UseDragon() //드래곤 사용
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
    IEnumerator TimeSlow() //슬로우 사용
{
    EnemyMove[] enemies = FindObjectsOfType<EnemyMove>();

    foreach (var move in enemies)
    {
        move.speed = Mathf.Max(0, move.speed -1);
    }

    yield return new WaitForSeconds(TimeSlowLast);

    foreach (var move in enemies)
    {
        move.speed += 1f;
    }
}
IEnumerator TeamBuff() //팀버프 사용
{
    ArcherTower[] archer = FindObjectsOfType<ArcherTower>();
    MageTower[] mage = FindObjectsOfType<MageTower>();
    WarriorTower[] warrior = FindObjectsOfType<WarriorTower>();

    foreach (var aRange in archer)
    {
        aRange.range += 2f;
    }
    foreach (var mRange in archer)
    {
         mRange.range = 2f;
    }
    foreach (var wRadius in warrior)
    {
        wRadius.radius = 2f;
    }

    yield return new WaitForSeconds(TeamBuffLast);

    foreach (var aRange in archer)
    {
        aRange.range -= 2f;
    }
    foreach (var mRange in archer)
    {
         mRange.range -= 2f;
    }
    foreach (var wRadius in warrior)
    {
        wRadius.radius -= 2f;
    }
}
}