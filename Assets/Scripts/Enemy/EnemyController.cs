using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [SerializeField] Transform _target;
    [SerializeField] Player _player;
    [SerializeField] GameObject _enemy;
    [SerializeField] GameObject _enemySpawnPos1;
    [SerializeField] GameObject _enemySpawnPos2;
    [SerializeField] int _minEnemy; //���߿� �����
    [SerializeField] int _maxEnemy; //���߿� �����

    public GameObject[] enemys;
    public List<GameObject> enemyList = new List<GameObject>();

    void Start() //���̺� ������ �׽�Ʈ �� �� ���
    {
        SpawnEnemy();
    }

    public void SpawnEnemy()
    {
        // ���̺� ������ �� �Լ� ����
        // ���̺� ������ ���� �� �װ�(����Ʈ�� �ƹ��͵� ���� ��) ������ð� �� ������
        // ���̺� ���� ���� csv���� �����ͼ�
        // ���� ���� = ���� or csv

        int enemyCount = Random.Range(_minEnemy, _maxEnemy); // ������ �ƴ϶� csv���Ϸ�
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
