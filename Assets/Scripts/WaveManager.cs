using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utils;

public class WaveManager : MonoBehaviour
{
    // ΩÃ±€≈Ê
    Player _player;
    public Player Player { set { _player = value; } }

    IngameUI _ingameUI;    // uiManager ΩÃ±€≈Ê ¿ÃøÎ
    public IngameUI IngameUI { set { _ingameUI = value; } }

    GameObject _shopUI;     // uiManager ΩÃ±€≈Ê ¿ÃøÎ
    public GameObject ShopUI { set { _shopUI = value; } }

    EnemyController _enemyController;
    public EnemyController EnemyController { set { _enemyController = value; } }

    int _wave;
    public int Wave { get { return _wave; } }

    float _waveTime;
    public float WaveTime { get { return _waveTime; } }
    float _nextWaveTime;
    bool _isClear;

    void Start()
    {
        _wave = 5;
        _nextWaveTime = 10f;
        _waveTime = _nextWaveTime;
        _isClear = true;
        _ingameUI.ShowWave(_wave);
        _ingameUI.TimeSkipInfoKey.SetActive(true);
        //GenericSingleton<ShopManager>.Instance.SpawnShop();
    }

    void Update()
    {
        CheckTime();
    }

    public void CheckTime()
    {
        if (_isClear)
        {
            if (_player.SkipTime() && _waveTime > 3f)
            {
                _waveTime = 3f;
                _ingameUI.ShowNextWaveTime(_waveTime);
                _ingameUI.TimeSkipInfoKey.SetActive(false);
            }
            if (_waveTime > 0)
            {
                if (_waveTime < 4f)
                    _ingameUI.TimeSkipInfoKey.SetActive(false);
                _waveTime -= Time.deltaTime;
                _ingameUI.ShowNextWaveTime(_waveTime);
            }
            else
                StartCoroutine(WaveRoutine());
        }
    }

    IEnumerator WaveRoutine()
    {
        _isClear = false;
        _enemyController.SpawnEnemy();
        GenericSingleton<ShopManager>.Instance.Shop.SetActive(false);
        _shopUI.SetActive(false);   //uiManager ΩÃ±€≈Ê ¿ÃøÎ
        _player.OpenShop = false;
        while (true)
        {
            if (_enemyController.EnemyList.Count == 0)
            {
                _isClear = true;
                _waveTime = _nextWaveTime;
                _wave++;
                _ingameUI.ShowWave(_wave);
                _ingameUI.TimeSkipInfoKey.SetActive(true);
                GenericSingleton<ShopManager>.Instance.Shop.SetActive(true);
                break;
            }
            else
            {
                yield return new WaitForSeconds(0.3f);
            }
        }
    }
}