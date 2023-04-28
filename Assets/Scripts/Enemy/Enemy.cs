using System.Collections;
using UnityEngine;
using Utils;

public class Enemy : MonoBehaviour
{
    [SerializeField] protected int _maxHp;
    [SerializeField] protected int _damage;
    [SerializeField] protected float _speed;
    [SerializeField] protected float _attackDelay;
    [SerializeField] protected int _money;

    protected EnemyController _enemyController;
    protected Animator _animator;
    protected Rigidbody _rigidbody;

    protected Vector3 _move;

    protected int _curHp;
    protected bool _isAttack;
    protected bool _isHitted;
    protected bool _isDie;
    protected bool _isMiss;

    public virtual void Init(EnemyController enemyController)
    {
        _enemyController = enemyController;
        _curHp = _maxHp;
        _enemyController.EnemyList.Add(this.gameObject);
    }

    public void LookTarget()
    {
        if (!_isDie)
        {
            _move = GenericSingleton<WaveManager>.Instance.Player.transform.position - transform.position;
            transform.LookAt(transform.position + _move);
        }
    }

    public virtual void Move()
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

    public void MoveAgain()
    {
        _isHitted = false;
    }

    public void EndDie()
    {
        Destroy(this.gameObject);
    }

    public virtual void Attack()
    {
        if (!_isAttack)
            StartCoroutine(AttackRoutine());
    }

    public void FreezePos()
    {
        _rigidbody.velocity = Vector3.zero;
    }

    protected virtual IEnumerator AttackRoutine()
    {
        _isAttack = true;
        yield return new WaitForSeconds(_attackDelay / 2);
        _animator.SetBool("isAttack", true);
        yield return new WaitForSeconds(0.3f);
        if (!_isMiss)
        {
            GenericSingleton<WaveManager>.Instance.Player.TakeDamage(_damage);
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
        if (other.gameObject.CompareTag("Player"))
            Attack();
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
            _isMiss = true;
    }
}
