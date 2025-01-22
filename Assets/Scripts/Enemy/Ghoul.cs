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
        base.EndDie();
        for (int i = 0; i < _enemyController.EnemyList.Count; i++)
        {
            if (_enemyController.EnemyList[i].GetComponent<Enemy>().IsDie == false)
                _enemyController.EnemyList[i].GetComponent<Enemy>().HealHP();   // 죽으면 남은 몬스터들 피 회복
        }
    }
}
