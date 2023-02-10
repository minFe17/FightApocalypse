using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
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

    int _curHp;

    void Start()
    {
        _animator= GetComponent<Animator>();
        _curHp = _maxHp;
    }

    public void Init()
    {

    }

    void Update()
    {
        Move();
        Turn();
        Fire();
    }

    public void Move()
    {
        // x = Ű���� A,D  z = Ű���� W,S
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");
        _move = new Vector3(x, 0, z);
        if(_move.magnitude > 0f)
        {
            transform.Translate(_move.normalized * Time.deltaTime * _moveSpeed, Space.World);
            _animator.SetBool("isWalk", true);
        }
        else
        {
            _animator.SetBool("isWalk", false);
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
        if(Input.GetButtonDown("Fire"))
        {
            GameObject bullet = Instantiate(_bullet);
            bullet.transform.position = _bulletPos.position;
            bullet.transform.rotation = _bulletPos.rotation;
        }
    }

    public void TakeDamage(int damage)
    {
        _curHp -= damage;
        if(_curHp <= 0) 
        {
            gameObject.SetActive(false);
        }
        else
        {

        }
    }
}
