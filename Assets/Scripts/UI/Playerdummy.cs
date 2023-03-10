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
    public int coin;
    public int maxCoin;
    int _money2;
    int _curHp2;
    float _curMoveSpeed2;

    bool _isDodge2;
    bool _isFire2;
    bool _iDown;

    GameObject nearObject;

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
    
    void GetInput()
    {
        //_iDown = Input.GetButtonDown("Walk");
        _iDown = Input.GetButtonDown("Interation");
    }
    
    void Interation()
    {
        Debug.Log("idown : "+_iDown+", nearObject"+(nearObject==null));
        if(_iDown && nearObject != null )
        {
            if (nearObject.tag =="Shop")
            {
                Shop shop = nearObject.GetComponent <Shop>();
                shop.Enter(this);
                //Destroy(nearObject);
            }
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
}
