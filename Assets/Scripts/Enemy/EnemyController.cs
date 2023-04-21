using System.Collections.Generic;
using UnityEngine;
using Utils;

public class EnemyController : MonoBehaviour
{
    [SerializeField] Transform _target;
    [SerializeField] Player _player;
    [SerializeField] GameObject _enemySpawnPos1;
    [SerializeField] GameObject _enemySpawnPos2;
    [SerializeField] IngameUI _ingameUI;

    List<GameObject> _enemyList = new List<GameObject>();
    public List<GameObject> EnemyList
    {
        get
        {
            return _enemyList;
        }
        set
        {
            _enemyList = value;
        }
    }

    List<GameObject> _enemys = new List<GameObject>();

    EEnemyType enemyType;

    int enemyCount;
    int zombieCount;
    int raptorCount;
    int pachyCount;
    int bossCount;

    void Start()
    {
        for (int i = 0; i < (int)EEnemyType.Max; i++)
        {
            _enemys.Add(Resources.Load($"Prefabs/{(EEnemyType)i}") as GameObject);
        }
        GenericSingleton<WaveManager>.Instance.EnemyController = this;
    }

    public void SpawnEnemy()
    {
        foreach (stEnemyData data in GenericSingleton<WaveEnemyData>.Instance.LstEnemyData)
        {
            if (data.WAVE == GenericSingleton<WaveManager>.Instance.Wave)
            {
                enemyCount = data.TOTALENEMY;
                zombieCount = data.ZOMBIE;
                raptorCount = data.RAPTOR;
                pachyCount = data.PACHY;
                bossCount = data.BOSS;
            }
        }

        for (int i = 0; i < enemyCount; i++)
        {
            Vector3 spawnPos = GetRandomSpawnPosition();
            SpawnEnemyType();
            GameObject enemy = Instantiate(_enemys[(int)enemyType], spawnPos, Quaternion.identity);
            enemy.GetComponent<Enemy>().Init(this, _target, _player, _ingameUI);
        }
        _ingameUI.ShowEnemy(enemyCount);
    }

    Vector3 GetRandomSpawnPosition()
    {
        int ramdom = Random.Range(0, 2);
        Vector3 basePos = new Vector3();
        Vector3 size = new Vector3();
        switch (ramdom)
        {
            case 0:
                {
                    basePos = _enemySpawnPos1.transform.position;
                    size = _enemySpawnPos1.GetComponent<BoxCollider>().size;
                }
                break;
            case 1:
                {
                    basePos = _enemySpawnPos2.transform.position;
                    size = _enemySpawnPos2.GetComponent<BoxCollider>().size;
                }
                break;
        }

        float posX = basePos.x + Random.Range(-size.x / 2f, size.x / 2f);
        float posZ = basePos.z + Random.Range(-size.z / 2f, size.z / 2f);
        Vector3 spawnPos = new Vector3(posX, basePos.y, posZ);
        return spawnPos;
    }

    void SpawnEnemyType()
    {
        if (zombieCount != 0)
        {
            zombieCount--;
            enemyType = EEnemyType.Zombie;
        }
        else if (raptorCount != 0)
        {
            raptorCount--;
            enemyType = EEnemyType.Raptor;
        }
        else if (pachyCount != 0)
        {
            pachyCount--;
            enemyType = EEnemyType.Pachy;
        }
        else if (bossCount != 0)
        {
            bossCount--;
            enemyType = EEnemyType.Boss;
        }
    }

    public void DieEnemy(GameObject enemy)
    {
        _enemyList.Remove(enemy);
        _ingameUI.ShowEnemy(_enemyList.Count);
    }
}

public enum EEnemyType
{
    Zombie,
    Raptor,
    Pachy,
    Boss,
    Max,
}
