using System.Collections;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] Camera _camera;
    [SerializeField] int _maxHp;
    [SerializeField] float _moveSpeed;
    [SerializeField] Transform _bulletPos;

    Animator _animator;
    Rigidbody _rigidbody;
    GameObject _bullet;
    Vector3 _move;

    int _money;
    int _curHp;
    float _curMoveSpeed;
    float _skipButtonDownTime;

    bool _isDodge;
    bool _isFire;
    bool _isDie;

    void Start()
    {
        _animator = GetComponent<Animator>();
        _rigidbody = GetComponent<Rigidbody>();
        _bullet = Resources.Load("Prefabs/Bullet") as GameObject;
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
        FreezePos();
    }

    public void Move()
    {
        // x = 키보드 A,D  z = 키보드 W,S
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");
        _move = new Vector3(x, 0, z);
        MoveAnimatoin(x, z);

        if (_move.magnitude > 0f)
            transform.Translate(_move.normalized * Time.deltaTime * _curMoveSpeed, Space.World);
    }

    public void MoveAnimatoin(float x, float z)
    {
        float y = transform.rotation.eulerAngles.y;
        if (y < 45 || y > 315)          // 위를 보고있을 때
        {
            _animator.SetFloat("AxisX", x);
            _animator.SetFloat("AxisZ", z);
        }
        else if (y > 45 && y < 135)     //오른쪽을 보고있을 때
        {
            _animator.SetFloat("AxisX", -z);
            _animator.SetFloat("AxisZ", x);
        }
        else if (y > 135 && y < 225)    //아래쪽을 보고있을 때
        {
            _animator.SetFloat("AxisX", -x);
            _animator.SetFloat("AxisZ", -z);
        }
        else                            //왼쪽을 보고있을 때
        {
            _animator.SetFloat("AxisX", z);
            _animator.SetFloat("AxisZ", -x);
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
        if (_isDodge || _isDie)
            return;
        _curHp -= damage;
        if (_curHp <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        _isDie = true;
        _animator.SetTrigger("doDie");
    }

    void DieEnd()
    {
        Time.timeScale = 0;
    }

    public bool SkipTime()
    {
        if (Input.GetKey(KeyCode.E))
            _skipButtonDownTime += Time.deltaTime;
        else
            _skipButtonDownTime -= Time.deltaTime;

        if (_skipButtonDownTime >= 1f)
        {
            _skipButtonDownTime = 0f;
            return true;
        }
        else if (_skipButtonDownTime < 0f)
        {
            _skipButtonDownTime = 0f;
            return false;
        }
        else
            return false;
    }

    public void GetMoney(int money)
    {
        _money += money;
    }

    public void FreezePos()
    {
        _rigidbody.velocity = Vector3.zero;
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
