using System.Collections;
using UnityEditor.UI;
using UnityEngine;

public class ItemManager : MonoBehaviour
{
    public float giveInterval = 1f;
    public float startDelay = 1f;

    void Start() 
    {
        StartCoroutine(GiveItemLoop());
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
        if (InventoryManager.Instance == null) return;
        if (InventoryManager.Instance.currentItem == ItemType.None) {
            int rand = Random.Range(1, 5);
            ItemType item = (ItemType)rand;

            InventoryManager.Instance.AddItem(item);
        }
        else
        {
            Debug.Log("아이템이 이미 있습니다");
        }
    }
}