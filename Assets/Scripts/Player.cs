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
    GameObject nearObject;
    Vector3 _move;

    int _potion;
    int _speedPotion;
    int _maxPotion;
    int _maxSpeedPotion;
    public int _money;
    int _curHp;
    float _curMoveSpeed;
    float _skipButtonDownTime;

    bool _isDodge;
    bool _isFire;
    bool _isDie;
    bool _iDown;
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
        Interation();
        GetInput();
    }

    public void Move()
    {
        // x = Ű���� A,D  z = Ű���� W,S
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
        if (y < 45 || y > 315)          // ���� �������� ��
        {
            _animator.SetFloat("AxisX", x);
            _animator.SetFloat("AxisZ", z);
        }
        else if (y > 45 && y < 135)     //�������� �������� ��
        {
            _animator.SetFloat("AxisX", -z);
            _animator.SetFloat("AxisZ", x);
        }
        else if (y > 135 && y < 225)    //�Ʒ����� �������� ��
        {
            _animator.SetFloat("AxisX", -x);
            _animator.SetFloat("AxisZ", -z);
        }
        else                            //������ �������� ��
        {
            _animator.SetFloat("AxisX", z);
            _animator.SetFloat("AxisZ", -x);
        }
    }

    public void Turn()
    {
        //���콺 ��ġ�� �ٶ󺸰� ȸ��
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
    /// �߰� �κ�
    void GetInput()
    {
        _iDown = Input.GetButtonDown("Interation");
    }

    void Interation()
    {      
        if (_iDown && nearObject != null)
        {
            if (nearObject.tag == "Shop")
            {
                Shop shop = nearObject.GetComponent<Shop>();
                shop.Enter(this);              
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Item")
        {
            Item item = other.GetComponent<Item>();
            Inventory.instance.AddItem(item);
            switch (item.itemType)
            {
                //case ItemType.Ammo:
                //    _ammo += item.value;
                //    if (_ammo > _maxAmmo)
                //        _ammo = _maxAmmo;
                //    break;
                //case ItemType.Coin:
                //    _coin += item.value;
                //    if (_coin > _maxCoin)
                //        _coin = _maxCoin;
                //    break;
                case ItemType.Potion:
                    _potion += item.value;
                    if (_potion > _maxPotion)
                        _potion = _maxPotion;
                    break;
                case ItemType.SpeedPotion:
                    _speedPotion += item.value;
                    if (_speedPotion > _maxSpeedPotion)
                        _speedPotion = _maxSpeedPotion;
                    break;

                //case ItemType.Grenade:
                //    _grenade += item.value;
                //    if (_grenade > _maxGrenade)
                //        _grenade = _maxGrenade;
                //    break;
            }
            Destroy(other.gameObject);
        }
    }

    void OnTriggerStay(Collider other)
    {
        if (other.tag == "Weapon" || other.tag == "Shop")
            nearObject = other.gameObject;
    }

    void OnTriggerExit(Collider other)
    {
        if (other.tag == "Weapon")
            nearObject = null;
        else if (other.tag == "Shop")
        {
            Shop shop = nearObject.GetComponent<Shop>();
            shop.Exit();
            nearObject = null;
        }
    }
    /// 

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
