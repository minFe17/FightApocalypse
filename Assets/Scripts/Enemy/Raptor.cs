using UnityEngine;
using Utils;

public class Raptor : Enemy
{
    [SerializeField] GameObject _attackArea;

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
            _animator.SetTrigger("doHit");
        }
    }

    public override void Attack()
    {
        _attackArea.SetActive(true);
    }

    public override void EndAttack()
    {
        _attackArea.SetActive(false);
        _isAttack = false;
    }
}
