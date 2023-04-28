using System.Collections;
using UnityEngine;
using Utils;

public class WaveManager : MonoBehaviour
{
    // ΩÃ±€≈Ê
    Player _player;
    public Player Player { get { return _player; } }

    EnemyController _enemyController;
    public EnemyController EnemyController { set { _enemyController = value; } }

    int _wave = 1;
    public int Wave { get { return _wave; } }

    float _waveTime;
    public float WaveTime { get { return _waveTime; } }
    float _nextWaveTime = 2 * 60f;
    bool _isClear = true;

    void Update()
    {
        CheckTime();
    }

    public void StartGame()
    {
        _waveTime = _nextWaveTime;
        GenericSingleton<UIManager>.Instance.CreateUI();
        GenericSingleton<UIManager>.Instance.IngameUI.ShowWave();
        GenericSingleton<UIManager>.Instance.IngameUI.TimeSkipInfoKey.SetActive(true);
        GenericSingleton<ShopManager>.Instance.SpawnShop();
        SpawnPlayer();
        CreateMiniMap();
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

    IEnumerator WaveRoutine()
    {
        _isClear = false;
        _enemyController.SpawnEnemy();
        GenericSingleton<ShopManager>.Instance.Shop.SetActive(false);
        if (_player.OpenShop == true)
        {
            GenericSingleton<ShopManager>.Instance.Shop.GetComponentInChildren<Shop>().Exit();
            _player.OpenShop = false;
        }
        while (true)
        {
            if (_enemyController.EnemyList.Count == 0)
            {
                _isClear = true;
                _waveTime = _nextWaveTime;
                _wave++;
                GenericSingleton<UIManager>.Instance.IngameUI.ShowWave();
                GenericSingleton<UIManager>.Instance.IngameUI.TimeSkipInfoKey.SetActive(true);
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