using UnityEngine;
using UnityEngine.UI;
using Utils;

public class MainUI : MonoBehaviour
{
    [SerializeField] IngameUI _ingameUI;
    [SerializeField] UIminiMap _miniMapUI;
    [SerializeField] GameObject _shopUI;
    [SerializeField] GameObject _gameOverUI;
    [SerializeField] InventoryUI _inventoryUI;
    [SerializeField] Text _talkText;
    [SerializeField] RectTransform _uiGroup;

    public void Init()
    {
        GenericSingleton<UIManager>.Instance.IngameUI = _ingameUI;
        GenericSingleton<UIManager>.Instance.MiniMapUI = _miniMapUI;
        GenericSingleton<UIManager>.Instance.ShopUI = _shopUI;
        GenericSingleton<UIManager>.Instance.GameOverUI = _gameOverUI;
        GenericSingleton<UIManager>.Instance.InventoryUI = _inventoryUI;
        GenericSingleton<UIManager>.Instance.TalkText= _talkText;
        GenericSingleton<UIManager>.Instance.ShopUIGroup = _uiGroup;
    }
}
