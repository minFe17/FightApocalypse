using UnityEngine;
using UnityEngine.UI;
using Utils;

public class IngameUI : MonoBehaviour
{
    [SerializeField] Text _waveText;
    [SerializeField] Text _waveInfoText;
    [SerializeField] GameObject _bossHpBarBase;
    [SerializeField] Image _bossHpBar;
    [SerializeField] Image _playerHpBar;
    [SerializeField] Text _moneyText;
    [SerializeField] GameObject _timeSkipInfoKey;
    [SerializeField] GameObject _openShopInfoKey;
    [SerializeField] Image _timeSkipKeyDownTime;

    public GameObject TimeSkipInfoKey { get { return _timeSkipInfoKey; } }
    public GameObject OpenShopInfoKey { get { return _openShopInfoKey; } }
    
    public void ShowWave()
    {
        int wave = GenericSingleton<WaveManager>.Instance.Wave;
        _waveText.text = $"Wave : {wave}";
    }

    public void ShowNextWaveTime()
    {
        int min = (int)GenericSingleton<WaveManager>.Instance.WaveTime / 60;
        int sec = (int)GenericSingleton<WaveManager>.Instance.WaveTime % 60;
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

    public void ShowSkipKeyButtonDownTime(float curButtonCoolTime, float maxButtonCoolTime)
    {
        _timeSkipKeyDownTime.fillAmount =  curButtonCoolTime / maxButtonCoolTime;
    }
}