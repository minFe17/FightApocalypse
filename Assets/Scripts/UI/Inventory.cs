using System;
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

    public delegate void OnSlotCoutnChange();
    public OnSlotCoutnChange onSlotCountChange;

    public delegate void OnChangeItem();
    public OnChangeItem onChangeItem;

    public List<Item> items = new List<Item>();

    private int slotCnt;
    public int SlotCnt
    {
        get => slotCnt;
            //화살표 함수 여러줄로 바꾸는법.  get다른곳에서 SLotCnt 읽을때 slotCnt 반환 set 다른곳에서 SlotCnt에 값을 넣어줄때 slotCnt에 받은값을 넣음  
        set{
            slotCnt =value;
            onSlotCountChange?.Invoke();
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
}
