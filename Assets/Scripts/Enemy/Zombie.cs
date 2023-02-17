using System.Collections;
using UnityEngine;

public class Zombie : Enemy
{
    void Start()
    {
        _animator = GetComponent<Animator>();
    }

    void Update()
    {
        LookTarget();
        Move();
    }

    public void HideEnemy() //���� �ִϸ��̼� ������ ����
    {
        gameObject.SetActive(false);
    }
}
