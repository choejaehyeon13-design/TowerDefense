using System.Collections;
using UnityEngine;

public class ItemManager : MonoBehaviour
{
    public static ItemManager Instance;
    public float giveInterval = 5f;
    public float startDelay = 1f;
    public ItemType currentItem = ItemType.None;
    public MageTower cooldown;
    public float dragonRadius = 3f;
    public int dragonDamage = 3;
    public float TimeSlowLast = 3f;
    public float TeamBuffLast = 2f;
    public int giveUpgradeCost = 10;
    void Awake()
    {
        Instance = this;
        
    }
    void Start() 
    {
        StartCoroutine(GiveItemLoop());

    }
    void Update() //스페이스로 아이템 사용
    {
        if (currentItem != ItemType.None && Input.GetKeyDown(KeyCode.Space))
        {
            UseItem();
        }
        
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
        InventoryManager.Instance.setInven(ItemType.None);
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
        foreach (var mRange in mage)
        {
            mRange.range += 2f;
        }
        foreach (var wRadius in warrior)
        {
            wRadius.radius += 2f;
        }

        yield return new WaitForSeconds(TeamBuffLast);

        foreach (var aRange in archer)
        {
            aRange.range -= 2f;
        }
        foreach (var mRange in mage)
        {
            mRange.range -= 2f;
        }
        foreach (var wRadius in warrior)
        {
            wRadius.radius -= 2f;
        }
    }
    IEnumerator GiveItemLoop() //아이템 지급 딜레이
    {
        yield return new WaitForSeconds(startDelay);

        while (true)
        {
            yield return new WaitForSeconds(giveInterval);
            GiveRandomItem();
        }
    }

    void GiveRandomItem() //아이템 랜덤 지급
    {
        if (currentItem != ItemType.None)
        {
            Debug.Log("이미 아이템 있음");
            return;
        }

        int rand = Random.Range(1, 5);
        ItemType item = (ItemType)rand;

        currentItem = item;

        InventoryManager.Instance.setInven(item);
        Debug.Log("아이템 지급: " + item);
    }
    public void GiveUpgrade()
    {
        if (giveInterval > 1f)
        {
            if (Gold.Instance.UseGold(giveUpgradeCost)){
                giveInterval = Mathf.Max(1f, giveInterval - 1f);
                Debug.Log("지급 주기 감소");
            }
        }
        else {
            Debug.Log("최대 업그레이드");
        }
    }
}