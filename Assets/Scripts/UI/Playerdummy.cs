using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Playerdummy : MonoBehaviour
{
    [SerializeField] Camera _camera2;
    [SerializeField] int _maxHp2;
    [SerializeField] float _moveSpeed2;
    //[SerializeField] GameObject _bullet2;
    //[SerializeField] Transform _bulletPos2;

    //Animator _animator2;
    Vector3 _move2;

    public GameObject[] weapons;
    public bool[] hasWeapons;
    public GameObject[] grenades;
    public int hasGrenades;
    public GameObject grenadeObj;
    public int _ammo;
    public int _coin;
    public int _potion;
    public int _speedPotion;
    public int _grenade;

    public int _maxAmmo;
    public int _maxCoin;
    public int _maxPotion;
    public int _maxSpeedPotion;
    public int _maxGrenade;

    int _money2;
    int _curHp2;
    float _curMoveSpeed2;

    bool _isDodge2;
    bool _isFire2;
    bool _iDown;
    bool _fDown;
    bool _openShop2;
    bool _gDown;
    bool _isFireReady;
    GameObject nearObject;   
    float fireDelay;

    void Start()
    {
        //_animator2 = GetComponent<Animator>();
        _curHp2 = _maxHp2;
        _curMoveSpeed2 = _moveSpeed2;
        _money2 = 0;
    }

    public void Init()
    {

    }

    void Update()
    {
        Move2();
        Turn2();
        //Fire2();
        Dodge2();
        Interation();
        GetInput();
        Grenade();
        Attack();
    }

    public void Move2()
    {
        // x = 키보드 A,D  z = 키보드 W,S
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");
        _move2 = new Vector3(x, 0, z);
        if (_move2.magnitude > 0f)
        {
            transform.Translate(_move2.normalized * Time.deltaTime * _curMoveSpeed2, Space.World);
            //_animator2.SetBool("isWalk", true);
        }
        else
        {
            //_animator2.SetBool("isWalk", false);
        }
    }

    public void Turn2()
    {
      //마우스 위치를 바라보게 회전
        Ray ray = _camera2.ScreenPointToRay(Input.mousePosition);
        RaycastHit rayHit;
        if (Physics.Raycast(ray, out rayHit, 100))
        {
            Vector3 lookDirection = rayHit.point - transform.position;
           lookDirection.y = 0;
            transform.LookAt(transform.position + lookDirection);
        }
    }

    //public void Fire2()
    //{
    //    if (Input.GetButton("Fire") && !_isFire2)
    //    {
    //        StartCoroutine(FireRoutine());
    //    }
    //}

    public void Dodge2()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift) && !_isDodge2)
        {
            _isDodge2 = true;
            _curMoveSpeed2 *= 2;
            Invoke("ReturnMoveSpeed", 0.5f);
        }
    }

    public void ReturnMoveSpeed()
    {
        Debug.Log(1);
        _isDodge2 = false;
        _curMoveSpeed2 = _moveSpeed2;
    }

    public void TakeDamage(int damage)
    {
        if (_isDodge2)
            return;
        _curHp2 -= damage;
        if (_curHp2 <= 0)
        {
            gameObject.SetActive(false);
        }
        else
        {

        }
    }

    public void GetMoney(int money)
    {
        _money2 += money;
    }

    //IEnumerator FireRoutine()
    //{
    //    _isFire2 = true;
    //    _animator2.SetTrigger("doShot");
    //    GameObject bullet = Instantiate(_bullet2);
    //    bullet.transform.position = _bulletPos2.position;
    //    bullet.transform.rotation = _bulletPos2.rotation;
    //    yield return new WaitForSeconds(0.3f);
    //    _isFire2 = false;
    //}

    
    /// ////////////////////////////////////////////////////////////
    
    void Attack()
    {
        if(_gDown)
        {
            //equipWeapon.Use();
        }
    }
    void Grenade()
    {
        if (hasGrenades == 0)
            return;
        if(_gDown )
        {
            Ray ray = _camera2.ScreenPointToRay(Input.mousePosition);
            RaycastHit rayHit;
            if (Physics.Raycast(ray, out rayHit, 100))
            {
                Vector3 lookDirection = rayHit.point - transform.position;
                lookDirection.y = 2;

                GameObject instantGrenade = Instantiate(grenadeObj, transform.position, transform.rotation);
                Rigidbody rigidGrenade = instantGrenade.GetComponent<Rigidbody>();
                rigidGrenade.AddForce(lookDirection, ForceMode.Impulse);
                rigidGrenade.AddTorque(Vector3.back * 10, ForceMode.Impulse);

                hasGrenades--;
                grenades[hasGrenades].SetActive(false);
            }
        }
    }
    void GetInput()
    {
        //_iDown = Input.GetButtonDown("Walk");
        _iDown = Input.GetButtonDown("Interation");
        _gDown = Input.GetButton("Fire2");
        _fDown = Input.GetButtonDown("Fire1");
    }
    
    void Interation()
    {
        //Debug.Log("idown : "+_iDown+", nearObject"+(nearObject==null));
        if(_iDown && nearObject != null )
        {
            if (nearObject.tag =="Shop")
            {
                dummyShop dummyshop = nearObject.GetComponent <dummyShop>();
                dummyshop.Enter(this);
                Destroy(nearObject);
            }
            if(nearObject.tag =="Weapon")
            {
                Item item = nearObject.GetComponent<Item>();
                int weaponIndex = item.value;
                hasWeapons[weaponIndex] = true;

                Destroy(nearObject);
                 
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Item")
        {
            Item item = other.GetComponent<Item>();
            Inventory.instance.AddItem(item);
            switch(item.itemType)
            {
                case ItemType.Ammo:
                    _ammo += item.value;
                    if (_ammo > _maxAmmo)
                        _ammo = _maxAmmo;
                    break;
                case ItemType.Coin:
                    _coin += item.value;
                    if (_coin > _maxCoin)
                        _coin = _maxCoin;
                    break;
                case ItemType.Potion:
                    _potion += item.value;
                    if (_potion > _maxPotion)
                        _potion = _maxPotion;
                    break;
                case ItemType.SpeedPotion:
                    _speedPotion += item.value;
                    if(_speedPotion> _maxSpeedPotion)
                        _speedPotion= _maxSpeedPotion;
                    break;

                case ItemType.Grenade:
                    _grenade += item.value;
                    if (_grenade > _maxGrenade)
                        _grenade = _maxGrenade;
                    break;
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
            dummyShop dummyshop = nearObject.GetComponent<dummyShop>();
            dummyshop.Exit();
            _openShop2 = false;       
            nearObject = null;
        }
   }
}
