using UnityEngine;

public class Zombie : Enemy
{
    [SerializeField] GameObject _attackArea2;

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

    public override void Attack()
    {
        _attackArea.SetActive(true);
        _attackArea2.SetActive(true);
    }

    public override void EndAttack()
    {
        _attackArea.SetActive(false);
        _attackArea2.SetActive(false);
        _isAttack = false;
    }
}
