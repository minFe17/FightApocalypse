using UnityEngine;



    public enum ItemType
    {
        Equipment,
        Consumables,
        Etc,
        Coin,
    }

[System.Serializable]
    public class Item 
    {
        public ItemType itemType;
        public int value;
        public string itemName;
        public Sprite itemImage;
        public bool Use()
        {
            return false;
        }
    }


