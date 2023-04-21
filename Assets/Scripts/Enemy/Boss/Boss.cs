using UnityEngine;

public class Boss : Enemy
{
    [SerializeField] BoxCollider _attackCollider;

    public EAttackType _attackType;

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

    public override void Init(EnemyController enemyController, Transform target, Player player, IngameUI ingameUi)
    {
        base.Init(enemyController, target, player, ingameUi);
        _attackCollider.enabled = false;
        _ingameUi.ShowBossHpBar(_curHp, _maxHp);
    }

    public override void Attack()
    {
        if (!_isAttack)
        {
            _isAttack = true;
            RandomAttack();
        }
    }

    void RandomAttack()
    {
        _attackType = (EAttackType)Random.Range(1, (int)EAttackType.Max);
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

    void OnAttackArea()     // 공격 애니메이션에서 호출
    {
        _attackCollider.enabled = true;
    }

    void OffAttackArea()    // 공격 애니메이션에서 호출
    {
        _attackCollider.enabled = false;
        _isAttack = false;
    }

    public override void TakeDamage(int damage)
    {
        if (_isDie)
            return;
        _curHp -= damage;
        _ingameUi.ShowBossHpBar(_curHp, _maxHp);

        if (_curHp <= 0)
        {
            _animator.SetTrigger("doDie");
            _isDie = true;
            _enemyController.DieEnemy(this.gameObject);
            _ingameUi.HideBossHpBar();
            _player.GetMoney(_money);
        }
        else
        {
            _isHitted = true;
            _animator.SetTrigger("doHit");
        }
    }
}

public enum EAttackType
{
    None,
    RightSlice,
    BothHands,
    Max,
}
