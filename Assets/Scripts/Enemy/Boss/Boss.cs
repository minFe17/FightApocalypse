using UnityEngine;
using Utils;

public class Boss : Enemy
{
    [SerializeField] BoxCollider _attackCollider;

    public int Damage { get { return _damage; } }

    EAttackType _attackType;
    public EAttackType AttackType { get { return _attackType; } }

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
        _attackCollider.enabled = false;
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

    void OnAttackArea()
    {
        _attackCollider.enabled = true;
    }

    void OffAttackArea()
    {
        _attackCollider.enabled = false;
        _isAttack = false;
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
}

public enum EAttackType
{
    None,
    RightSlice,
    BothHands,
    Max,
}
