using System.Collections;
using UnityEngine;
using Utils;

public class Player : MonoBehaviour
{
    [SerializeField] int _maxHp;
    [SerializeField] float _moveSpeed;
    [SerializeField] float _maxMoveSpeed;
    [SerializeField] Transform _bulletPos;

    AudioClip _shotAudio;
    Animator _animator;
    Rigidbody _rigidbody;
    Camera _camera;
    GameObject _bullet;
    GameObject nearObject;

    UIManager _uiManager;
    Option _optionUI;
    Vector3 _move;

    int _potion;
    int _speedPotion;
    int _maxPotion;
    int _maxSpeedPotion;
    int _money;
    int _curHp;
    float _curMoveSpeed;
    float _skipButtonDownTime;
    float _dodgeCoolTime;

    bool _isDodge;
    bool _isFire;
    bool _isDie;
    bool _iDown;
    bool _openShop;

    public int Money { get { return _money; } set { _money = value; } }
    public bool OpenShop { get { return _openShop; } set { _openShop = value; } }
    public bool IsDie { get { return _isDie; } }

    void Start()
    {
        CreateCamera();
        _animator = GetComponent<Animator>();
        _rigidbody = GetComponent<Rigidbody>();
        _bullet = Resources.Load("Prefabs/Bullet") as GameObject;
        _shotAudio = Resources.Load("Prefabs/ShotAudio") as AudioClip;
        _curHp = _maxHp;
        _curMoveSpeed = _moveSpeed;
        _uiManager = GenericSingleton<UIManager>.Instance;
        _optionUI = _uiManager.OptionUI;
        _uiManager.InventoryUI._Player = this;
        _uiManager.IngameUI.ShowPlayerHpBar(_curHp, _maxHp);
        _uiManager.IngameUI.ShowMoney(_money);
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

    public void Move()  // Blend Tree를 활용하여 플레이어 애니메이션 실행
    {
        if (_isDie)
            return;
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");
        _move = new Vector3(x, 0, z);

        _animator.SetFloat("Rotation", transform.rotation.eulerAngles.y);
        _animator.SetFloat("AxisX", x);
        _animator.SetFloat("AxisZ", z);

        if (_move.magnitude > 0f)
            _rigidbody.velocity = _move.normalized * _curMoveSpeed;
    }

    public void Turn()  // Ray를 사용하여 플레이어 방향 조정
    {
        if (_isDie)
            return;
        Ray ray = _camera.ScreenPointToRay(Input.mousePosition);    
        RaycastHit rayHit;
        if (Physics.Raycast(ray, out rayHit, 100))
        {
            Vector3 lookDirection = rayHit.point - transform.position;
            lookDirection.y = 0;
            transform.LookAt(transform.position + lookDirection);
        }
    }

    public void Dodge()     // Dodge
    {
        if (_isDie)
            return;
        if (_dodgeCoolTime < 2f)    // Dodge 쿨타임 체크
        {
            _dodgeCoolTime += Time.deltaTime;
            return;
        }
        if (Input.GetKeyDown(KeyCode.LeftShift) && !_isDodge)
        {
            float y = Quaternion.FromToRotation(Vector3.forward, _move).eulerAngles.y;

            if (Mathf.Abs(transform.rotation.eulerAngles.y - y) <= 30 || Mathf.Abs(transform.rotation.eulerAngles.y - y) >= 330)    // 캐릭터 방향과 일치하는지 체크
            {
                _animator.SetTrigger("doDodge");
                _animator.SetFloat("Rotation", transform.rotation.eulerAngles.y);
                _curMoveSpeed *= 2;
                _isDodge = true;
                _dodgeCoolTime = 0;
            }
        }
    }

    public void Fire()
    {
        if (_isDie)
            return;
        if (Input.GetButton("Fire") && !_isFire && !_openShop && !_uiManager.OptionUI.OpenOption)
        {
            StartCoroutine(FireRoutine());
        }
    }

    public void SpendMoney()
    {
        _money -= 100;
    }

    public void HealHP()
    {
        if (_maxHp <= _curHp)
            return;
        _curHp += 20;
        if (_maxHp < _curHp)
            _curHp = _maxHp;

        _uiManager.IngameUI.ShowPlayerHpBar(_curHp, _maxHp);
    }

    public void UpSpeed()
    {
        if (_maxMoveSpeed <= _curMoveSpeed)
            return;
        _curMoveSpeed += 2;
        if (_maxMoveSpeed < _curMoveSpeed)
            _curMoveSpeed = _maxMoveSpeed;
    }

    public void EndDodge()
    {
        _isDodge = false;
        _curMoveSpeed /= 2f;
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
        _uiManager.IngameUI.ShowPlayerHpBar(_curHp, _maxHp);

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
                _uiManager.IngameUI.OpenShopInfoKey.SetActive(false);
                _uiManager.IngameUI.TimeSkipInfoKey.SetActive(false);
                _openShop = true;
            }
        }
    }

    void Die()
    {
        _isDie = true;
        _animator.SetTrigger("doDie");
    }

    void DieEnd()
    {
        _uiManager.GameOverUI.SetActive(true);
        _uiManager.GameOverUI.GetComponent<GameOverUI>().ShowGameOverWave(GenericSingleton<WaveManager>.Instance.Wave);
        GenericSingleton<SoundManager>.Instance.SoundController.StopBGM();
        Time.timeScale = 0;
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
        GenericSingleton<SoundManager>.Instance.SoundController.PlayFBXAudio(_shotAudio);
        yield return new WaitForSeconds(0.3f);
        _isFire = false;
    }

    public bool SkipTime()
    {
        if (Input.GetKey(KeyCode.F))
        {
            _skipButtonDownTime += Time.deltaTime;
            _uiManager.IngameUI.ShowSkipKeyButtonDownTime(_skipButtonDownTime, 1f);
        }
        else
        {
            _skipButtonDownTime -= Time.deltaTime;
            _uiManager.IngameUI.ShowSkipKeyButtonDownTime(_skipButtonDownTime, 1f);
        }

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
        _uiManager.IngameUI.ShowMoney(_money);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Item"))
        {
            _animator.SetTrigger("doPickUpItem");
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
            _uiManager.IngameUI.OpenShopInfoKey.SetActive(true);
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
            _uiManager.IngameUI.OpenShopInfoKey.SetActive(false);
            if (GenericSingleton<WaveManager>.Instance.WaveTime > 3f)
                _uiManager.IngameUI.TimeSkipInfoKey.SetActive(true);
            _openShop = false;
            nearObject = null;
        }
    }
}