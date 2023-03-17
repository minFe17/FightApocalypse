using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    #region Singleton
    public static Inventory instance;
    private void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
            return;
        }
        instance = this;
    }
    #endregion

    public delegate void OnSlotCoutnChange(int val);
    public OnSlotCoutnChange onSlotCountChange;

    public delegate void OnChangeItem();
    public OnChangeItem onChangeItem;

    public List<Item> items = new List<Item>();

    private int slotCnt;
    public int SlotCnt
    {
        get => slotCnt;
        set{
            slotCnt = value;
            onSlotCountChange.Invoke(slotCnt);
        }
    }

    void Start()
    {
        SlotCnt = 4;
    }

    public bool AddItem(Item _item)
    {
        if(items.Count<SlotCnt)
        {
            items.Add(_item);
            if(onChangeItem !=null)
            onChangeItem.Invoke();
            return true;    
        }
        return false;
    }

    void OnTriggerEnter(Collider collision)
    { 
        if(collision.CompareTag("FieldItem"))
        {
            Debug.Log("input item");
            FieldItems fieldItems = collision.GetComponent<FieldItems>();
            if (AddItem(fieldItems.GetItem()))
                fieldItems.DestroyItem();
        }
        
    }

    
    void Update()
    {
        
    }
}
