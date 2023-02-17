using System.Collections;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] Camera _camera;
    [SerializeField] int _maxHp;
    [SerializeField] float _moveSpeed;
    [SerializeField] GameObject _bullet;
    [SerializeField] Transform _bulletPos;

    Animator _animator;
    Vector3 _move;

    int _money;
    int _curHp;
    float _curMoveSpeed;

    bool _isDodge;
    bool _isFire;

    void Start()
    {
        _animator = GetComponent<Animator>();
        _curHp = _maxHp;
        _curMoveSpeed = _moveSpeed;
        _money = 0;
    }

    public void Init()
    {

    }

    void Update()
    {
        Move();
        Turn();
        Fire();
        Dodge();
    }

    public void Move()
    {
        // x = 키보드 A,D  z = 키보드 W,S
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");
        _move = new Vector3(x, 0, z);
        if (_move.magnitude > 0f)
        {
            transform.Translate(_move.normalized * Time.deltaTime * _curMoveSpeed, Space.World);
            _animator.SetBool("isWalk", true);
        }
        else
        {
            _animator.SetBool("isWalk", false);
        }
    }

    public void Turn()
    {
        //마우스 위치를 바라보게 회전
        Ray ray = _camera.ScreenPointToRay(Input.mousePosition);
        RaycastHit rayHit;
        if (Physics.Raycast(ray, out rayHit, 100))
        {
            Vector3 lookDirection = rayHit.point - transform.position;
            lookDirection.y = 0;
            transform.LookAt(transform.position + lookDirection);
        }
    }

    public void Fire()
    {
        if (Input.GetButton("Fire") && !_isFire)
        {
            StartCoroutine(FireRoutine());
        }
    }

    public void Dodge()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift) && !_isDodge)
        {
            _isDodge = true;
            _curMoveSpeed *= 2;
            Invoke("ReturnMoveSpeed", 0.5f);
        }
    }

    public void ReturnMoveSpeed()
    {
        _isDodge = false;
        _curMoveSpeed = _moveSpeed;
    }

    public void TakeDamage(int damage)
    {
        if (_isDodge)
            return;
        _curHp -= damage;
        if (_curHp <= 0)
        {
            gameObject.SetActive(false);
        }
        else
        {

        }
    }

    public void GetMoney(int money)
    {
        _money += money;
    }

    IEnumerator FireRoutine()
    {
        _isFire = true;
        _animator.SetTrigger("doShot");
        GameObject bullet = Instantiate(_bullet);
        bullet.transform.position = _bulletPos.position;
        bullet.transform.rotation = _bulletPos.rotation;
        yield return new WaitForSeconds(0.3f);
        _isFire = false;
    }
}
