using UnityEngine;
using Utils;

public class ShopManager : MonoBehaviour
{
    // ΩÃ±€≈Ê
    GameObject _shop;
    public GameObject Shop { get { return _shop; } }

    public void SpawnShop()
    {
        GameObject temp = Resources.Load("Prefabs/ItemShop") as GameObject;
        _shop = Instantiate(temp);
        _shop.GetComponentInChildren<Shop>().UIGroup = GenericSingleton<UIManager>.Instance.ShopUIGroup;
        _shop.GetComponentInChildren<Shop>().TalkText = GenericSingleton<UIManager>.Instance.TalkText;
    }
}
