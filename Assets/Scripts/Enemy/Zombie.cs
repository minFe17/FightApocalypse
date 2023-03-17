using System.Collections;
using UnityEngine;

public class Zombie : Enemy
{
    void Start()
    {
        _animator = GetComponent<Animator>();
    }

    void Update()
    {
        LookTarget();
        Move();
    }
}
