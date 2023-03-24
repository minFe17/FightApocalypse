using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class Zombie : Enemy
{
    void Start()
    {
        _animator = GetComponent<Animator>();
        _rigidbody = GetComponent<Rigidbody>();
    }

    void Update()
    {
        LookTarget();
        Move();
        FreezePos();
    }
}
