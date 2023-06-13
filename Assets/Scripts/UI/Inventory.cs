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

    Player player;
    public delegate void OnSlotCoutnChange();
    public OnSlotCoutnChange onSlotCountChange;

    public delegate void OnChangeItem();
    public OnChangeItem onChangeItem;

    public List<Item> items = new List<Item>();
    private int slotCnt;
    public int SlotCnt
    {
        get => slotCnt;
            //ȭ��ǥ �Լ� �����ٷ� �ٲٴ¹�.  get�ٸ������� SLotCnt ������ slotCnt ��ȯ set �ٸ������� SlotCnt�� ���� �־��ٶ� slotCnt�� �������� ����  
        set{
            slotCnt =value;
            onSlotCountChange?.Invoke();
        }
    }

    void Start()
    {
        SlotCnt = 4;
        player = GetComponent<Player>();
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
    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Alpha1))
        {
            for(int i = 0; i < items.Count; i++)
            {
                Debug.Log(items[i].itemType);
                if (items[i].itemType == ItemType.Potion)
                {
                    player.HealHP();
                    items.RemoveAt(i);
                    if (onChangeItem != null)
                        onChangeItem.Invoke();
                    break;
                }
            }
        }
        if(Input.GetKeyDown(KeyCode.Alpha2))
        {
            for(int i =0; i < items.Count;i++) 
            {
                if (items[i].itemType == ItemType.SpeedPotion)
                {
                    player.UpSpeed();
                    items.RemoveAt(i);
                    if (onChangeItem != null)
                        onChangeItem.Invoke();
                    break;
                }
            }
        }
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
