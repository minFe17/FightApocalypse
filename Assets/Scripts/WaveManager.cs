using System.Collections;
using UnityEngine;

public class WaveManager : MonoBehaviour
{
    [SerializeField] Player _player;
    [SerializeField] float _nextWaveTime;
    [SerializeField] EnemyController _enemyController;

    public int _wave;

    float _time;
    bool _isClear;

    void Start()
    {
        _wave = 1;
        _nextWaveTime *= 60;
        _time = _nextWaveTime;
        _isClear = true;
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
                _time = 5f;
            if (_time > 0)
                _time -= Time.deltaTime;
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
            yield return new WaitForSeconds(0.3f);
            if (_enemyController.enemyList.Count == 0)
            {
                _isClear = true;
                _time = _nextWaveTime;
                _wave++;
                break;
            }
        }
    }
}