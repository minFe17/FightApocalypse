using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FieldItems : MonoBehaviour
{
    public Item item;
    public SpriteRenderer image;      

    public void SetItem(Item _item)
    {
        item.itemNAme = _item.itemNAme;
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
