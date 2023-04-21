using UnityEngine;



    public enum ItemType
    {
        Equipment,
        Consumables,
        Ammo,
        Coin,
        Grenade,
        Potion,
        SpeedPotion,
        Weapon,
        Etc,
    }

[System.Serializable]
    public class Item : MonoBehaviour
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


