using UnityEngine;
using UnityEngine.UI;

public class ItemSlot : MonoBehaviour
{
    public Sprite[] itemSprites;
    public Image iconImage;

    public void SetItem(ItemType item)
    {
        if (item == ItemType.None)
        {
            iconImage.sprite = null;
            iconImage.enabled = false;
            return;
        }
        Debug.Log(iconImage);
        int index = (int)item;

        if (index >= 0 && index < itemSprites.Length)
        {
            iconImage.sprite = itemSprites[index];
            iconImage.enabled = true;
        }
    }
}