using UnityEngine;
using Utils;

public class Pachy : Enemy
{
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
            _animator.SetTrigger("doHit");
        }
    }

    public override void AttackReady()
    {
        _animator.SetTrigger("doReady");
    }

    public override void Attack()
    {
        _isAttack = true;
        _animator.SetTrigger("doAttack");
    }

    public override void EndAttack()
    {
        _isAttack = false;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player") && _isAttack)
            GenericSingleton<WaveManager>.Instance.Player.TakeDamage(_damage);
    }
}
