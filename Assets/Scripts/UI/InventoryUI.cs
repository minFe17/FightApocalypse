using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Utils;

public class InventoryUI : MonoBehaviour
{
    Inventory inven;
    Slot[] slots;
    [SerializeField] GameObject inventoryPanel;
    [SerializeField] Transform slotHolder;
    Player player;
    public Player _Player { set { player = value; } }
    bool activeInventory = false;

    private void Start()
    {
        inven = Inventory.instance;
        
        slots = slotHolder.GetComponentsInChildren<Slot>();
        inven.onSlotCountChange += SlotChange;
        inven.onChangeItem += RedrawSlotUI;
        inventoryPanel.SetActive(activeInventory);
        closeShop.onClick.AddListener(DeActiveShop);
    }

    private void SlotChange()
    {
        for (int i =0; i < slots.Length; i++) 
        {
            if (i < inven.SlotCnt)
                slots[i].GetComponent<Button>().interactable = true;
            else
                slots[i].GetComponent<Button>().interactable = false;
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            activeInventory = !activeInventory;
            inventoryPanel.SetActive(activeInventory);
        }
        //if (Input.GetMouseButtonUp(0))
        //    RayShop();
    }

    public void AddSlot()
    {
        inven.SlotCnt++;
        //player.SpendMoney();
        if (player != null)
        {
            if(player.Money >100)
            player.Money = -100;
        }

    }

    void RedrawSlotUI()
    {
        for(int i =0; i<slots.Length;i++) 
        {
            slots[i].RemoveSlot();
        }
        for(int i =0; i<inven.items.Count;i++)
        {
            slots[i].Item = inven.items[i];
            slots[i].UpdateSlotUI();
        }
    }

    public GameObject shop;
    public Button closeShop;

    //public void RayShop()
    //{
    //    Vector3 mousePos =Camera.main.ScreenToWorldPoint(Input.mousePosition);
    //    mousePos.z = -10;
    //    RaycastHit hit;// = Physics.Raycast(mousePos, transform.forward, 30);
    //    Ray ray = new Ray(mousePos, transform.forward);
    //    Debug.DrawRay(ray.origin, ray.direction * 3000, Color.red, 5f);
    //    if(Physics.Raycast(mousePos, transform.forward, out hit, 30))
    //    {
    //        if(hit.collider.CompareTag("Store"))
    //        {
    //            ActiveShop(true);
    //        }
    //    }
    //}
    //public void ActiveShop(bool isOpen)
    //{
    //    shop.SetActive(isOpen);
    //}
    public void DeActiveShop()
    {
        GenericSingleton<UIManager>.Instance.ShopUIGroup.anchoredPosition = Vector3.down * 1500;
    }
}
