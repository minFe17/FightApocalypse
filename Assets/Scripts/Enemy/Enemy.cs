using UnityEngine;
using Utils;

public class Enemy : MonoBehaviour
{
    [SerializeField] protected GameObject _attackArea;
    [SerializeField] protected int _maxHp;
    [SerializeField] protected int _damage;
    [SerializeField] protected float _speed;
    [SerializeField] protected float _attackDelay;

    protected EnemyController _enemyController;
    protected Animator _animator;
    protected Rigidbody _rigidbody;

    protected Vector3 _move;

    protected int _curHp;
    protected int _money;
    protected bool _isAttack;
    protected bool _isHitted;
    protected bool _isDie;
    protected bool _isMiss;

    public virtual void Init(EnemyController enemyController)
    {
        _enemyController = enemyController;
        _curHp = _maxHp;
        _enemyController.EnemyList.Add(this.gameObject);
        _money = Random.Range(1, 11) * 10;
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
            Debug.Log(1);
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

    public virtual void EndDie()
    {
        Destroy(this.gameObject);
        Debug.Log(2);
    }

    public virtual void AttackReady()
    {
        _animator.SetTrigger("doAttack");
        _isAttack = true;
    }

    public virtual void Attack()
    {
        _attackArea.SetActive(true);
    }

    public virtual void EndAttack()
    {
        _attackArea.SetActive(false);
        _isAttack = false;
    }

    public void FreezePos()
    {
        _rigidbody.velocity = Vector3.zero;
    }

    public void HealHP()
    {
        _curHp += 10;
        if(_curHp > _maxHp)
            _curHp = _maxHp;
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Player") && !_isAttack)
            AttackReady();
    }
}
