using UnityEngine;
using UnityEngine.UI;
using Utils;

public class UIManager : MonoBehaviour
{
    // ΩÃ±€≈Ê

    GameObject _ui;

    public IngameUI IngameUI { get; set; }
    public UIminiMap MiniMapUI { get; set; }
    public GameObject ShopUI { get; set; }
    public GameObject GameOverUI { get; set; }

    public Text TalkText { get; set; }
    public RectTransform ShopUIGroup { get; set; }

    public void CreateUI()
    {
        GameObject temp = Resources.Load("Prefabs/UI") as GameObject;
        _ui = Instantiate(temp);
        _ui.GetComponent<MainUI>().Init();
    }
}
