using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [SerializeField] Transform _target;
    [SerializeField] Player _player;
    [SerializeField] GameObject _enemy;
    [SerializeField] GameObject _enemySpawnPos1;
    [SerializeField] GameObject _enemySpawnPos2;
    [SerializeField] int _minEnemy; //나중에 지울거
    [SerializeField] int _maxEnemy; //나중에 지울거

    public GameObject[] enemys;
    public List<GameObject> enemyList = new List<GameObject>();

    void Start() //웨이브 전까지 테스트 할 때 사용
    {
        SpawnEnemy();
    }

    public void SpawnEnemy()
    {
        // 웨이브 시작할 때 함수 실행
        // 웨이브 시작은 몬스터 다 죽고(리스트에 아무것도 없을 때) 재정비시간 좀 지나고
        // 웨이브 몬스터 수는 csv파일 가져와서
        // 몬스터 생성 = 랜덤 or csv

        int enemyCount = Random.Range(_minEnemy, _maxEnemy); // 랜덤이 아니라 csv파일로
        for (int j = 0; j <= enemyCount; j++)
        {
            Vector3 spawnPos = GetRandomSpawnPosition();
            int random = Random.Range(0, enemys.Length);
            GameObject enemy = Instantiate(enemys[random], spawnPos, Quaternion.identity);
            enemy.GetComponent<Enemy>().Init(this, _target, _player);
        }
    }

    public Vector3 GetRandomSpawnPosition()
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
}
