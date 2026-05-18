using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Gold : MonoBehaviour
{
    public static Gold Instance;

    [Header("골드")]
    public int gold = 10;

    [Header("시간당 골드")]
    public int goldPerTick = 2;
    public float interval = 5f;

    [Header("골드 UI")]
    public Text goldText;

    void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        UpdateGoldUI();
        StartCoroutine(GoldRoutine());
    }

    IEnumerator GoldRoutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(interval);
            AddGold(goldPerTick);
        }
    }

    public bool UseGold(int amount)
    {
        if (gold >= amount)
        {
            gold -= amount;
            UpdateGoldUI();
            Debug.Log("골드 사용: " + amount + " / 남은 골드: " + gold);
            return true;
        }

        Debug.Log("골드 부족");
        return false;
    }

    public void AddGold(int amount)
    {
        gold += amount;
        UpdateGoldUI();
        Debug.Log("골드 획득: " + amount + " / 현재 골드: " + gold);
    }

    void UpdateGoldUI()
    {
        if (goldText != null)
        {
            goldText.text = "Gold : " + gold;
        }
    }
}