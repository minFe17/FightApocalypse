using UnityEngine;
using Utils;

public class ShopUI : MonoBehaviour
{
    public void Buy(int index)
    {
        GenericSingleton<ShopManager>.Instance.Shop.GetComponentInChildren<Shop>().Buy(index);
    }
}