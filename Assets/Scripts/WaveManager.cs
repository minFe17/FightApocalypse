using System.Collections;
using UnityEngine;
using Utils;

public class WaveManager : MonoBehaviour
{
    // ╫л╠шео
    Player _player;
    EnemyController _enemyController;
    int _wave;
    float _waveTime;
    float _nextWaveTime = 60f;
    bool _isClear;

    public Player Player { get { return _player; } }
    public EnemyController EnemyController { set { _enemyController = value; } }
    public int Wave { get { return _wave; } }
    public float WaveTime { get { return _waveTime; } }
    public bool IsNewGame { get; set; }

    void Update()
    {
        CheckTime();
    }

    public void StartGame()
    {
        _wave = 1;
        _waveTime = _nextWaveTime;
        _isClear = true;
        GenericSingleton<UIManager>.Instance.CreateUI();
        GenericSingleton<UIManager>.Instance.IngameUI.ShowWave();
        GenericSingleton<UIManager>.Instance.IngameUI.TimeSkipInfoKey.SetActive(true);
        GenericSingleton<ShopManager>.Instance.SpawnShop();
        SpawnPlayer();
        CreateMiniMap();
        GenericSingleton<SoundManager>.Instance.SoundController.StartBGM();
    }

    public void SpawnPlayer()
    {
        GameObject temp = Resources.Load("Prefabs/Player") as GameObject;
        GameObject player = Instantiate(temp);
        _player = player.GetComponent<Player>();
    }

    public void CreateMiniMap()
    {
        GameObject temp = Resources.Load("Prefabs/MiniMapCamera") as GameObject;
        GameObject miniMapCamera = Instantiate(temp);
        GenericSingleton<UIManager>.Instance.MiniMapUI.MiniMapCamera = miniMapCamera.GetComponent<Camera>();
    }

    public void CheckTime()
    {
        if (_isClear)
        {
            if (_player.SkipTime() && _waveTime > 3f)
            {
                _waveTime = 3f;
                GenericSingleton<UIManager>.Instance.IngameUI.ShowNextWaveTime();
                GenericSingleton<UIManager>.Instance.IngameUI.TimeSkipInfoKey.SetActive(false);
            }
            if (_waveTime > 0)
            {
                if (_waveTime < 4f)
                    GenericSingleton<UIManager>.Instance.IngameUI.TimeSkipInfoKey.SetActive(false);
                _waveTime -= Time.deltaTime;
                GenericSingleton<UIManager>.Instance.IngameUI.ShowNextWaveTime();
            }
            else
                StartCoroutine(WaveRoutine());
        }
    }

    public void EndGame()
    {
        GenericSingleton<UIManager>.Instance.GameClearUI.SetActive(true);
        Time.timeScale = 0;
    }

    IEnumerator WaveRoutine()
    {
        _isClear = false;
        IsNewGame = false;
        _enemyController.SpawnEnemy();
        GenericSingleton<ShopManager>.Instance.Shop.SetActive(false);
        GenericSingleton<UIManager>.Instance.IngameUI.ShowSkipKeyButtonDownTime(0, 1f);
        if (_player.OpenShop == true)
        {
            GenericSingleton<ShopManager>.Instance.Shop.GetComponentInChildren<Shop>().Exit();
            _player.OpenShop = false;
        }
        while (true)
        {
            if (Player.IsDie)
                break;
            if (_enemyController.EnemyList.Count == 0)
            {
                if (GenericSingleton<WaveEnemyData>.Instance.LstEnemyData.Count <= _wave)
                {
                    EndGame();
                    GenericSingleton<SoundManager>.Instance.SoundController.StopBGM();
                    break;
                }
                else
                {
                    _isClear = true;
                    _waveTime = _nextWaveTime;
                    _wave++;
                    GenericSingleton<UIManager>.Instance.IngameUI.ShowWave();
                    GenericSingleton<UIManager>.Instance.IngameUI.TimeSkipInfoKey.SetActive(true);
                    GenericSingleton<ShopManager>.Instance.Shop.SetActive(true);
                    break;
                }
            }
            else
            {
                yield return new WaitForSeconds(0.3f);
            }
        }
    }
}