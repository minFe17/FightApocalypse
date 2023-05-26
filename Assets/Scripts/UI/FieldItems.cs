using UnityEngine;

public class FieldItems : MonoBehaviour
{
    Item item;
    SpriteRenderer image;      

    public void SetItem(Item _item)
    {
        item.itemName = _item.itemName;
        item.itemImage = _item.itemImage;
        item.itemType = _item.itemType;

        image.sprite = _item.itemImage; 
    }
    public Item GetItem()
    {
        return item;
    }
    public void DestroyItem()
    {
        Destroy(gameObject);
    }
}
