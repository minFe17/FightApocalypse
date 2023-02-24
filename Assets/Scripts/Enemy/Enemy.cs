using System.Collections;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int _maxHp;
    public int _damage;
    public float _speed;
    public float _attackDelay;
    public int _money; //랜덤으로 할 수도?

    protected Player _player;
    protected EnemyController _enemyController;
    protected Transform _target;
    protected Animator _animator;

    protected Vector3 _move;

    protected int _curHp;
    protected bool _isAttack;
    protected bool _isHitted;
    protected bool _isDie;
    protected bool _isMiss;

    public void Init(EnemyController enemyController, Transform target, Player player)
    {
        _enemyController = enemyController;
        _target = target;
        _player = player;
        _curHp = _maxHp;

        _enemyController.enemyList.Add(this.gameObject);
    }

    public void LookTarget()
    {
        if (!_isDie)
        {
            _move = _target.position - transform.position;
            transform.LookAt(transform.position + _move);
        }
    }

    public void Move()
    {
        if (!_isHitted && !_isDie && !_isAttack)
        {
            _animator.SetBool("isWalk", true);
            transform.Translate(_move.normalized * Time.deltaTime * _speed, Space.World);
        }
    }

    public virtual void TakeDamage(int damage)
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
            Invoke("MoveAgain", 0.3f);
        }
    }

    public void MoveAgain()
    {
        _isHitted = false;
    }

    public void Attack()
    {
        if (!_isAttack)
            StartCoroutine(AttackRoutine());
    }

    protected virtual IEnumerator AttackRoutine()
    {
        _isAttack = true;
        yield return new WaitForSeconds(_attackDelay / 2);
        _animator.SetBool("isAttack", true);
        yield return new WaitForSeconds(0.3f);
        if (!_isMiss)
        {
            _player.TakeDamage(_damage);
            yield return new WaitForSeconds(0.5f);
            _animator.SetBool("isAttack", false);
            yield return new WaitForSeconds(_attackDelay / 2);
        }
        else
        {
            _animator.SetBool("isAttack", false);
            yield return new WaitForSeconds(0.5f);
            _isMiss = false;
        }

        _isAttack = false;

    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Player")
            Attack();
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
            _isMiss = true;
    }
}
