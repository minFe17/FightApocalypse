using System.Collections;
using UnityEngine;

public class Pachy : Enemy
{
    bool _isRush;
    void Start()
    {
        _animator = GetComponent<Animator>();
        _rigidbody = GetComponent<Rigidbody>();
    }

    void Update()
    {
        LookTarget();
        Move();
        Rush();
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
            _enemyController.enemyList.Remove(this.gameObject);
            _player.GetMoney(_money);
        }
        else
        {
            _isHitted = true;
            _animator.SetTrigger("doHit");
        }
    }

    public void Rush()
    {
        if (_isRush && !_isHitted && !_isDie)
        {
            _animator.SetBool("isReady", false);
            _animator.SetBool("isAttack", true);
            transform.Translate(_move.normalized * Time.deltaTime * _speed * 5, Space.World);
        }
    }

    protected override IEnumerator AttackRoutine()
    {
        _isAttack = true;
        yield return new WaitForSeconds(_attackDelay / 2);
        _animator.SetBool("isReady", true);
        yield return new WaitForSeconds(1f);
        _isRush = true;

        yield return new WaitForSeconds(0.5f);
        _animator.SetBool("isAttack", false);
        _isRush = false;
        yield return new WaitForSeconds(_attackDelay / 2);
        _isAttack = false;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player" && _isAttack)
            _player.TakeDamage(_damage);
    }
}
