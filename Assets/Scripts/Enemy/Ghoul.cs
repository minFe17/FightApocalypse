using UnityEngine;
using Utils;

public class Ghoul : Enemy
{
    public int Damage { get { return _damage; } }

    void Start()
    {
        _animator = GetComponent<Animator>();
        _rigidbody = GetComponent<Rigidbody>();
    }

    void Update()
    {
        LookTarget();
        Move();
        FreezePos();
    }

    public override void TakeDamage(int damage)
    {
        if (_isDie)
            return;
        _curHp -= damage;

        if (_curHp <= 0)
        {
            _animator.SetTrigger("doDie");
            _isDie = true;
            _enemyController.DieEnemy(this.gameObject);
            GenericSingleton<WaveManager>.Instance.Player.GetMoney(_money);
        }
        else
        {
            _isHitted = true;
            Invoke("MoveAgain", 0.3f);
        }
    }

    public override void EndDie()
    {
        // 부모 클래스의 EndDie 함수 실행
        base.EndDie();

        // 살아있는 적들의 체력을 회복시킴 (자신이 죽을 때 발동)
        for (int i = 0; i < _enemyController.EnemyList.Count; i++)
        {
            GameObject enemyObject = _enemyController.EnemyList[i];
            Enemy enemy = enemyObject.GetComponent<Enemy>();

            if (enemy != null && !enemy.IsDie)
                enemy.HealHP();
        }
    }
}
