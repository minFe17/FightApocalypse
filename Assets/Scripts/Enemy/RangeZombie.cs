using UnityEngine;
using Utils;

public class RangeZombie : Enemy
{
    [SerializeField] GameObject _attackArea2;

    GameObject _poisonBallPrefab;
    PoisonBall _poisonBall;

    public int Damage { get { return _damage; } }

    void Start()
    {
        _animator = GetComponent<Animator>();
        _rigidbody = GetComponent<Rigidbody>();
        _poisonBallPrefab = Resources.Load("Prefabs/Enemy/PoisonBall") as GameObject;
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

    public void MakeBullet(int index)
    {
        if (index == 0)
        {
            GameObject temp = Instantiate(_poisonBallPrefab, _attackArea.transform.position, _attackArea.transform.rotation);
            _poisonBall = temp.GetComponent<PoisonBall>();
            _poisonBall.Init(_attackArea.transform);
        }
        else
        {
            GameObject temp = Instantiate(_poisonBallPrefab, _attackArea2.transform.position, _attackArea2.transform.rotation);
            _poisonBall = temp.GetComponent<PoisonBall>();
            _poisonBall.Init(_attackArea2.transform);
        }
    }

    public void ThrowBullet()
    {
        _poisonBall.ThrownBullet();
    }

    public override void EndAttack()
    {
        _isAttack = false;
    }

}
