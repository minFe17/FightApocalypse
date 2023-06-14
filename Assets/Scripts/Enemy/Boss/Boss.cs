using UnityEngine;
using Utils;

public class Boss : Enemy
{
    EAttackType _attackType;

    public EAttackType AttackType { get { return _attackType; } }
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

    public override void Init(EnemyController enemyController)
    {
        base.Init(enemyController);
        GenericSingleton<UIManager>.Instance.IngameUI.ShowBossHpBar(_curHp, _maxHp);
    }

    public override void Move()
    {
        if (!_isDie && !_isAttack)
        {
            _animator.SetBool("isWalk", true);
            transform.Translate(_move.normalized * Time.deltaTime * _speed, Space.World);
        }
    }

    public override void AttackReady()
    {
        if (!_isAttack)
        {
            Debug.Log(1);
            _isAttack = true;
            RandomAttack();
        }
    }

    void RandomAttack()
    {
        _attackType = (EAttackType)Random.Range(0, (int)EAttackType.Max);
        switch (_attackType)
        {
            case EAttackType.RightSlice:
                _animator.SetTrigger("doAttack1");
                break;
            case EAttackType.BothHands:
                _animator.SetTrigger("doAttack2");
                break;
        }
    }

    public override void TakeDamage(int damage)
    {
        if (_isDie)
            return;
        _curHp -= damage;
        GenericSingleton<UIManager>.Instance.IngameUI.ShowBossHpBar(_curHp, _maxHp);

        if (_curHp <= 0)
        {
            _animator.SetTrigger("doDie");
            _isDie = true;
            _enemyController.DieEnemy(this.gameObject);
            GenericSingleton<UIManager>.Instance.IngameUI.HideBossHpBar();
            GenericSingleton<WaveManager>.Instance.Player.GetMoney(_money);
        }
        else
        {
            _isHitted = true;
            _animator.SetTrigger("doHit");
        }
    }

    public override void HealHP()
    {
        base.HealHP();
        GenericSingleton<UIManager>.Instance.IngameUI.ShowBossHpBar(_curHp, _maxHp);
    }
}

public enum EAttackType
{
    RightSlice,
    BothHands,
    Max,
}
