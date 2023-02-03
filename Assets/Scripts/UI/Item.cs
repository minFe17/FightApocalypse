using UnityEngine;

public enum ItemType
{
    Equipment,
    Consumables,
    Etc
}
[System.Serializable]
public class Item
{
    public ItemType itemType;
    public string itemNAme;
    public Sprite itemImage;
    public bool Use()
    {
        bool isUsed = false;
        isUsed= true;
        return false;
    }
}

