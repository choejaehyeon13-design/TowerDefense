using UnityEngine;

public class ItemSlot : MonoBehaviour
{
    public Sprite emptySprite;          
    public Sprite[] itemSprites;        

    public SpriteRenderer iconRenderer; 

    public void SetItem(ItemType item)
    {
        if (item == ItemType.None)
        {
            iconRenderer.sprite = emptySprite;
            return;
        }

        int index = (int)item;

        if (index >= 0 && index < itemSprites.Length)
        {
            iconRenderer.sprite = itemSprites[index];
        }
    }
}