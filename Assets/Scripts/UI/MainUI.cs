using UnityEngine;
using UnityEngine.UI;
using Utils;

public class MainUI : MonoBehaviour
{
    [SerializeField] IngameUI _ingameUI;
    [SerializeField] UIminiMap _miniMapUI;
    [SerializeField] GameObject _shopUI;
    [SerializeField] InventoryUI _inventoryUI;
    [SerializeField] GameObject _gameOverUI;
    [SerializeField] GameObject _gameClearUI;
    [SerializeField] Text _talkText;
    [SerializeField] RectTransform _uiGroup;

    public void Init()
    {
        GenericSingleton<UIManager>.Instance.IngameUI = _ingameUI;
        GenericSingleton<UIManager>.Instance.MiniMapUI = _miniMapUI;
        GenericSingleton<UIManager>.Instance.ShopUI = _shopUI;
        GenericSingleton<UIManager>.Instance.InventoryUI = _inventoryUI;
        GenericSingleton<UIManager>.Instance.GameOverUI = _gameOverUI;
        GenericSingleton<UIManager>.Instance.GameClearUI = _gameClearUI;
        GenericSingleton<UIManager>.Instance.TalkText = _talkText;
        GenericSingleton<UIManager>.Instance.ShopUIGroup = _uiGroup;
    }
}
