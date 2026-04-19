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

    IEnumerator GiveItemLoop()
    {
        yield return new WaitForSeconds(startDelay);

        while (true)
        {
            yield return new WaitForSeconds(giveInterval);
            GiveRandomItem();
        }
    }

    void GiveRandomItem()
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