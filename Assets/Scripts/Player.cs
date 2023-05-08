using System.Collections;
using UnityEngine;
using Utils;

public class Player : MonoBehaviour
{
    [SerializeField] int _maxHp;
    [SerializeField] float _moveSpeed;
    [SerializeField] Transform _bulletPos;

    Animator _animator;
    Rigidbody _rigidbody;
    Camera _camera;
    GameObject _bullet;
    GameObject nearObject;
    Vector3 _move;

    int _potion;
    int _speedPotion;
    int _maxPotion;
    int _maxSpeedPotion;
    int _money;
    int _curHp;
    float _curMoveSpeed;
    float _skipButtonDownTime;

    bool _isDodge;
    bool _isFire;
    bool _isDie;
    bool _iDown;
    bool _openShop;

    public int Money {  get { return _money; } set { _money = value; } }
    public bool OpenShop { get { return _openShop; } set { _openShop = value; } }

    void Start()
    {
        CreateCamera();
        _animator = GetComponent<Animator>();
        _rigidbody = GetComponent<Rigidbody>();
        _bullet = Resources.Load("Prefabs/Bullet") as GameObject;
        _curHp = _maxHp;
        _curMoveSpeed = _moveSpeed;
        _money = 0;
        GenericSingleton<UIManager>.Instance.IngameUI.ShowPlayerHpBar(_curHp, _maxHp);
        GenericSingleton<UIManager>.Instance.IngameUI.ShowMoney(_money);
    }

    void FixedUpdate()
    {
        Move();
    }

    void Update()
    {
        Turn();
        Fire();
        Dodge();
        FreezePos();
        Interation();
        GetInput();
    }

    public void Move()
    {
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");
        _move = new Vector3(x, 0, z);

        _animator.SetFloat("Rotation", transform.rotation.eulerAngles.y);
        _animator.SetFloat("AxisX", x);
        _animator.SetFloat("AxisZ", z);

        if (_move.magnitude > 0f)
            transform.Translate(_move.normalized * Time.deltaTime * _curMoveSpeed, Space.World);
    }

    public void Turn()
    {
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
        if (Input.GetButton("Fire") && !_isFire && !_openShop)
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
        GenericSingleton<UIManager>.Instance.IngameUI.ShowPlayerHpBar(_curHp, _maxHp);

        if (_curHp <= 0)
        {
            Die();
        }
    }
    /// 추가 부분
    void GetInput()
    {
        _iDown = Input.GetButtonDown("Interation");
    }

    void Interation()
    {
        if (_iDown && nearObject != null)
        {
            if (nearObject.CompareTag("Shop"))
            {
                Shop shop = nearObject.GetComponent<Shop>();
                shop.Enter(this);
                GenericSingleton<UIManager>.Instance.IngameUI.OpenShopInfoKey.SetActive(false);
                GenericSingleton<UIManager>.Instance.IngameUI.TimeSkipInfoKey.SetActive(false);
                _openShop = true;
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Item"))
        {
            Item item = other.GetComponent<Item>();
            Inventory.instance.AddItem(item);
            switch (item.itemType)
            {
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
            }
            Destroy(other.gameObject);
        }
        if (other.CompareTag("Shop"))
        {
            GenericSingleton<UIManager>.Instance.IngameUI.OpenShopInfoKey.SetActive(true);
        }
    }

    void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Weapon") || other.CompareTag("Shop"))
            nearObject = other.gameObject;
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Weapon"))
            nearObject = null;
        else if (other.CompareTag("Shop"))
        {
            Shop shop = nearObject.GetComponent<Shop>();
            shop.Exit();
            GenericSingleton<UIManager>.Instance.IngameUI.OpenShopInfoKey.SetActive(false);
            if (GenericSingleton<WaveManager>.Instance.WaveTime > 3f)
                GenericSingleton<UIManager>.Instance.IngameUI.TimeSkipInfoKey.SetActive(true);
            _openShop = false;
            nearObject = null;
        }
    }

    void Die()
    {
        _isDie = true;
        _animator.SetTrigger("doDie");
    }

    void DieEnd()
    {
        GenericSingleton<UIManager>.Instance.GameOverUI.SetActive(true);
        GenericSingleton<UIManager>.Instance.GameOverUI.GetComponent<GameOverUI>().ShowGameOverWave(GenericSingleton<WaveManager>.Instance.Wave);
        Time.timeScale = 0;
    }

    public bool SkipTime()
    {
        if (Input.GetKey(KeyCode.F))
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
        GenericSingleton<UIManager>.Instance.IngameUI.ShowMoney(_money);
    }

    void FreezePos()
    {
        _rigidbody.velocity = Vector3.zero;
    }

    void CreateCamera()
    {
        GameObject temp = Resources.Load("Prefabs/Main Camera") as GameObject;
        GameObject camera = Instantiate(temp);
        camera.GetComponent<MainCamera>().Target = gameObject.transform;
        _camera = camera.GetComponent<Camera>();
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
