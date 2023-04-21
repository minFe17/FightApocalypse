using System.Collections;
using UnityEngine;
using UnityEngine.AI;

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
    protected Rigidbody _rigidbody;
    protected IngameUI _ingameUi;

    protected Vector3 _move;

    protected int _curHp;
    protected bool _isAttack;
    protected bool _isHitted;
    protected bool _isDie;
    protected bool _isMiss;

    public virtual void Init(EnemyController enemyController, Transform target, Player player, IngameUI ingameUI)
    {
        _enemyController = enemyController;
        _target = target;
        _player = player;
        _ingameUi = ingameUI;
        _curHp = _maxHp;

        _enemyController.EnemyList.Add(this.gameObject);
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
        _curHp -= damage;

        if (_curHp <= 0)
        {
            _animator.SetTrigger("doDie");
            _isDie = true;
            _enemyController.DieEnemy(this.gameObject);
            _player.GetMoney(_money);
        }
        else
        {
            _isHitted = true;
            Invoke("MoveAgain", 0.3f);
        }
    }

    public void MoveAgain() //죽지 않으면 애니메이션 끝나고 실행
    {
        _isHitted = false;
    }

    public void EndDie()        //죽으면 애니메이션 끝나고 실행
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
