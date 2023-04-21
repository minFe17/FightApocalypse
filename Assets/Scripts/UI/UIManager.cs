using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Utils;

public class UIManager : MonoBehaviour
{
    // ½Ì±ÛÅæ (³ªÁß¿¡) 

    [SerializeField] RectTransform _uiGroup;
    [SerializeField] Text _talkText;
    [SerializeField] GameObject _shopUI;

    void Start()
    {
        GenericSingleton<ShopManager>.Instance.SpawnShop(_uiGroup, _talkText);
        GenericSingleton<WaveManager>.Instance.ShopUI = _shopUI;
    }
}
