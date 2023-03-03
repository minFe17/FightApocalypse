using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [SerializeField] Transform _target;
    [SerializeField] Player _player;
    [SerializeField] GameObject _enemySpawnPos1;
    [SerializeField] GameObject _enemySpawnPos2;
    [SerializeField] WaveManager _waveManager;
    [SerializeField] WaveEnemyData _waveEnemyData;
    [SerializeField] int _minEnemy; //나중에 지울거
    [SerializeField] int _maxEnemy; //나중에 지울거

    public List<GameObject> enemyList = new List<GameObject>();
    List<GameObject> enemys = new List<GameObject>();

    EEnemyType enemyType;

    int enemyCount;
    int zombieCount;
    int raptorCount;
    int pachyCount;

    void Start()
    {
        enemys.Add(Resources.Load("Prefabs/Zombie") as GameObject);
        enemys.Add(Resources.Load("Prefabs/Raptor") as GameObject);
        enemys.Add(Resources.Load("Prefabs/Pachy") as GameObject);
    }

    public void SpawnEnemy()
    {
        foreach (stEnemyData data in _waveEnemyData.lstEnemyData)
        {
            if (data.WAVE == _waveManager._wave)
            {
                enemyCount = data.TOTALENEMY;
                zombieCount = data.ZOMBIE;
                raptorCount = data.RAPTOR;
                pachyCount = data.PACHY;
            }
        }

        for (int i = 0; i < enemyCount; i++)
        {
            Vector3 spawnPos = GetRandomSpawnPosition();
            SpawnEnemyType();
            GameObject enemy = Instantiate(enemys[(int)enemyType], spawnPos, Quaternion.identity);
            enemy.GetComponent<Enemy>().Init(this, _target, _player);
        }

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
    }
}

public enum EEnemyType
{
    Zombie,
    Raptor,
    Pachy
}
