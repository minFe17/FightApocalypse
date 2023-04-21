using UnityEngine;
using UnityEngine.UI;

public class IngameUI : MonoBehaviour
{
    [SerializeField] Text _waveText;
    [SerializeField] Text _waveInfoText;
    [SerializeField] GameObject _bossHpBarBase;
    [SerializeField] Image _bossHpBar;
    [SerializeField] Image _playerHpBar;
    [SerializeField] Text _moneyText;
    [SerializeField] GameObject _timeSkipInfoKey;
    public GameObject TimeSkipInfoKey { get { return _timeSkipInfoKey; } }
    [SerializeField] GameObject _openShopInfoKey;
    public GameObject OpenShopInfoKey { get { return _openShopInfoKey; } }

    public void ShowWave(int wave)
    {
        _waveText.text = $"Wave : {wave}";
    }

    public void ShowNextWaveTime(float time)
    {
        int min = (int)time / 60;
        int sec = (int)time % 60;
        _waveInfoText.text = "정비 시간 : " + string.Format("{0:D2} : {1:D2}", min, sec);
    }

    public void ShowEnemy(int totalEnemy)
    {
        _waveInfoText.text = "남은 적 : " + string.Format("{0:D2}", totalEnemy);
    }

    public void ShowBossHpBar(int curHp, int maxHp)
    {
        if (_bossHpBarBase.activeSelf == false)
            _bossHpBarBase.SetActive(true);
        _bossHpBar.fillAmount = (float)curHp / maxHp;
    }

    public void HideBossHpBar()
    {
        _bossHpBarBase.SetActive(false);
    }

    public void ShowPlayerHpBar(int curHp, int maxHp)
    {
        _playerHpBar.fillAmount = (float)curHp / maxHp;
    }

    public void ShowMoney(int money)
    {
        _moneyText.text = $"$ {money}";
    }
}