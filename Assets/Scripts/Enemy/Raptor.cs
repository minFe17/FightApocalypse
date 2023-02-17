using System.Collections;
using UnityEngine;

public class Raptor : Enemy
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

    public override void TakeDamage(int damage)
    {
        if (_isDie)
            return;
        _curHp -= _damage;

        if (_curHp <= 0)
        {
            _animator.SetTrigger("doDie");
            _isDie = true;
            _enemyController.enemyList.Remove(this.gameObject);
            _player.GetMoney(_money);

        }
        else
        {
            _isHitted = true;
            _animator.SetTrigger("doHit");
            Invoke("MoveAgain", 0.3f);
        }
    }
}
