using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] Transform _target;
    [SerializeField] float _speed;

    void Start()
    {
        
    }

    void Update()
    {
        Vector3 move = _target.position - transform.position;
        transform.Translate(move.normalized * Time.deltaTime * _speed);
    }
}
