using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopManager : MonoBehaviour
{
    // ΩÃ±€≈Ê
    GameObject _shop;
    //RectTransform _uiGroup;
    //Text _talkText;
    public GameObject Shop {  get { return _shop; } }

    public void SpawnShop(RectTransform uiGroup, Text talkText)
    {
        GameObject temp = Resources.Load("Prefabs/ItemShop") as GameObject;
        _shop = Instantiate(temp);
        _shop.GetComponentInChildren<Shop>().UIGroup = uiGroup;
        _shop.GetComponentInChildren<Shop>().TalkText= talkText;
    }
}
