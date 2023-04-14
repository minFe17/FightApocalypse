using System.Collections;
using UnityEngine;

public class WaveManager : MonoBehaviour
{
    Player _player;
    public Player Player {  set { _player = value; } }

    IngameUIPanel _ingameUI;    // IngameUIPanel ½Ì±ÛÅæÀ¸·Î
    public IngameUIPanel IngameUI {  set { _ingameUI = value; } }

    EnemyController _enemyController;
    public EnemyController EnemyController { set { _enemyController = value; } }

    int _wave;
    public int Wave {  get { return _wave; } }

    float _time;
    float _nextWaveTime;
    bool _isClear;

    void Start()
    {
        _wave = 1;
        _nextWaveTime = 120f;
        _time = _nextWaveTime;
        _isClear = true;
        _ingameUI.ShowWave(_wave);
    }

    void Update()
    {
        CheckTime();
    }

    public void CheckTime()
    {
        if (_isClear)
        {
            if (_player.SkipTime() && _time > 3f)
            {
                _time = 3f;
                _ingameUI.ShowNextWaveTime(_time);
            }
            if (_time > 0)
            {
                _time -= Time.deltaTime;
                _ingameUI.ShowNextWaveTime(_time);
            }
            else
                StartCoroutine(WaveRoutine());
        }
    }

    IEnumerator WaveRoutine()
    {
        _isClear = false;
        _enemyController.SpawnEnemy();
        while (true)
        {
            if (_enemyController.EnemyList.Count == 0)
            {
                _isClear = true;
                _time = _nextWaveTime;
                _wave++;
                _ingameUI.ShowWave(_wave);
                break;
            }
            else
            {
                yield return new WaitForSeconds(0.3f);
            }
        }
    }
}