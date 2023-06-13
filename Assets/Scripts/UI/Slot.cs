using UnityEngine;
using UnityEngine.UI;

public class Slot : MonoBehaviour
{
    private Item item;
    public Item Item { get { return item; } set { item = value; } }
    [SerializeField] Image itemIcon;

    public void UpdateSlotUI()
    {
        itemIcon.sprite = item.itemImage;
        itemIcon.gameObject.SetActive(true);
    }
    public void RemoveSlot()
    {
        item = null;
        itemIcon.gameObject.SetActive(false);
    }
}
