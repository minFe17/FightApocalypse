using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] float _moveSpeed;

    Animator _animator;

    void Start()
    {
        _animator= GetComponent<Animator>();
    }

    void Update()
    {
        Move();
    }

    public void Move()
    {
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");
        Vector3 move = new Vector3(x, 0, z);
        if(move.magnitude > 0f)
        {
            transform.Translate(move.normalized * Time.deltaTime * _moveSpeed);
            _animator.SetBool("isMove", true);
        }
        else
        {
            _animator.SetBool("isMove", false);
        }
    }
}
