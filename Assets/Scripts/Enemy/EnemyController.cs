using System.Collections.Generic;
using UnityEngine;
using Utils;

public class EnemyController : MonoBehaviour
{
    [SerializeField] List<GameObject> _enemySpawnPos = new List<GameObject>();

    List<GameObject> _enemyList = new List<GameObject>();
    List<GameObject> _enemys = new List<GameObject>();

    public List<GameObject> EnemyList { get { return _enemyList; } set { _enemyList = value; } }

    EEnemyType enemyType;

    int _enemyCount;
    int _zombieCount;
    int _explsionZombieCount;
    int _ghoulCount;
    int _raptorCount;
    int _pachyCount;
    int _bossCount;

    void Start()
    {
        for (int i = 0; i < (int)EEnemyType.Max; i++)
        {
            _enemys.Add(Resources.Load($"Prefabs/{(EEnemyType)i}") as GameObject);
        }
        GenericSingleton<WaveManager>.Instance.StartGame();
        GenericSingleton<WaveManager>.Instance.EnemyController = this;
    }

    public void SpawnEnemy()
    {
        foreach (stEnemyData data in GenericSingleton<WaveEnemyData>.Instance.LstEnemyData)
        {
            if (data.WAVE == GenericSingleton<WaveManager>.Instance.Wave)
            {
                _enemyCount = data.TOTALENEMY;
                _zombieCount = data.ZOMBIE;
                _explsionZombieCount = data.EXPLSIONZOMBIE;
                _ghoulCount = data.GHOUL;
                _raptorCount = data.RAPTOR;
                _pachyCount = data.PACHY;
                _bossCount = data.BOSS;
            }
        }

        for (int i = 0; i < _enemyCount; i++)
        {
            Vector3 spawnPos = GetRandomSpawnPosition();
            SpawnEnemyType();
            GameObject enemy = Instantiate(_enemys[(int)enemyType], spawnPos, Quaternion.identity);
            enemy.GetComponent<Enemy>().Init(this);
        }
        GenericSingleton<UIManager>.Instance.IngameUI.ShowEnemy(_enemyList.Count);
    }

    Vector3 GetRandomSpawnPosition()
    {
        int ramdom = Random.Range(0, _enemySpawnPos.Count);

        Vector3 basePos = _enemySpawnPos[ramdom].transform.position;
        Vector3 size = _enemySpawnPos[ramdom].GetComponent<BoxCollider>().size;

        float posX = basePos.x + Random.Range(-size.x / 2f, size.x / 2f);
        float posZ = basePos.z + Random.Range(-size.z / 2f, size.z / 2f);
        Vector3 spawnPos = new Vector3(posX, basePos.y, posZ);
        return spawnPos;
    }

    void SpawnEnemyType()
    {
        if (_zombieCount != 0)
        {
            _zombieCount--;
            enemyType = EEnemyType.Zombie;
        }
        else if (_explsionZombieCount != 0)
        {
            _explsionZombieCount--;
            enemyType = EEnemyType.ExplsionZombie;
        }
        else if (_ghoulCount != 0)
        {
            _ghoulCount--;
            enemyType = EEnemyType.Ghoul;
        }
        else if (_raptorCount != 0)
        {
            _raptorCount--;
            enemyType = EEnemyType.Raptor;
        }
        else if (_pachyCount != 0)
        {
            _pachyCount--;
            enemyType = EEnemyType.Pachy;
        }
        else if (_bossCount != 0)
        {
            _bossCount--;
            enemyType = EEnemyType.Boss;
        }
    }

    public void DieEnemy(GameObject enemy)
    {
        _enemyList.Remove(enemy);
        GenericSingleton<UIManager>.Instance.IngameUI.ShowEnemy(_enemyList.Count);
    }
}

public enum EEnemyType
{
    Zombie,
    ExplsionZombie,
    Ghoul,
    Raptor,
    Pachy,
    Boss,
    Max,
}
