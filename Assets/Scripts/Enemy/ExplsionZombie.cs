using UnityEngine;
using Utils;

public class ExplsionZombie : Enemy
{
    GameObject _explsion;   //Á×À¸¸é ¼ÒÈ¯
    
    public int Damage { get { return _damage; } }

    void Start()
    {
        _animator = GetComponent<Animator>();
        _rigidbody = GetComponent<Rigidbody>();
        _explsion = Resources.Load("Prefabs/Explsion") as GameObject;
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
            for (int i = 0; i < _enemyController.EnemyList.Count; i++)
            {
                _enemyController.EnemyList[i].GetComponent<Enemy>().HealHP();
            }
        }
        else
        {
            _isHitted = true;
            Invoke("MoveAgain", 0.3f);
        }
    }

    public override void EndDie()
    {
        base.EndDie();
        GameObject temp = Instantiate(_explsion);
        temp.transform.position = transform.position;
    }
}
