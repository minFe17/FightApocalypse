using System.Collections;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] int _maxHp;
    [SerializeField] int _damage;
    [SerializeField] float _speed;
    [SerializeField] int _money; //랜덤으로 할 수도?

    Player _player;
    EnemyController _enemyController;
    Transform _target;
    Animator _animator;

    Vector3 _move;

    int _curHp;

    bool _isAttack;
    bool _isHitted;
    bool _isDie;

    void Start()
    {
        _animator = GetComponent<Animator>();
        _curHp = _maxHp;
    }

    public void Init(EnemyController enemyController, Transform target, Player player)
    {
        _enemyController = enemyController;
        _target = target;
        _player = player;
        _curHp = _maxHp;

        _enemyController.enemyList.Add(this.gameObject);
    }

    void Update()
    {
        LookTarget();
        Move();
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

    public void TakeDamage(int damage)
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

    public void HideEnemy()
    {
        gameObject.SetActive(false);
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Player" && !_isAttack)
            StartCoroutine(AttackRoutine());
    }

    IEnumerator AttackRoutine()
    {
        _isAttack = true;
        yield return new WaitForSeconds(0.3f);
        _animator.SetBool("isAttack", true);
        yield return new WaitForSeconds(0.3f);
        _player.TakeDamage(_damage);
        yield return new WaitForSeconds(0.5f);
        _animator.SetBool("isAttack", false);
        yield return new WaitForSeconds(0.5f);
        _isAttack = false;
    }

    //Collider 한개 더 만들거나 Raycsat
    //_player.GetComponent<Player>().TakeDamage(_damage);
}
