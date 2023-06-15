using UnityEngine;
using Utils;

public class Pachy : Enemy
{
    float _pachyMoveSpeed;

    void Start()
    {
        _animator = GetComponent<Animator>();
        _rigidbody = GetComponent<Rigidbody>();
        _pachyMoveSpeed = _speed;
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
        _animator.setBool("doReady");
    }

    public override void Attack()
    {
        _isAttack = true;
        _animator.SetTrigger("doAttack");
        _speed *= 2f;
    }

    public override void EndAttack()
    {
        _isAttack = false;
        _speed = _pachyMoveSpeed;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player") && _isAttack)
            GenericSingleton<WaveManager>.Instance.Player.TakeDamage(_damage);
    }
}
