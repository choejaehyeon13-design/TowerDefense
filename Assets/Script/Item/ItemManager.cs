using System.Collections;
using UnityEngine;

public class ItemAutoGiver : MonoBehaviour
{
    public float giveInterval = 5f; // 지급 주기 (초)
    public float StartDelay = 10f;

    void Start()
    {
        StartCoroutine(GiveItemLoop());
    }

    IEnumerator GiveItemLoop()
    {
        yield return new WaitForSeconds(StartDelay); // 시작 지연
        while (true)
        {
            yield return new WaitForSeconds(giveInterval);

            GiveRandomItem();
        }
    }

    void GiveRandomItem()
    {
        if (InventoryManager.Instance == null) return;

        int rand = Random.Range(0, 4); // 0~3

        ItemType item = (ItemType)rand;

        InventoryManager.Instance.AddItem(item);
    }
}