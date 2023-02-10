using System.Collections;
using System.Collections.Generic;
using TreeEditor;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] Transform _target;
    [SerializeField] Player _player;
    [SerializeField] int _damage;
    [SerializeField] float _speed;

    void Start()
    {
        
    }

    void Init()
    {

    }

    void Update()
    {
        Move();
    }

    public void Move()
    {
        Vector3 move = _target.position - transform.position;
        transform.Translate(move.normalized * Time.deltaTime * _speed, Space.World);

        transform.LookAt(transform.position + move);
    }

    public void TakeDamage(int damage)
    {
        
    }
    //Collider 한개 더 만들거나 Raycsat
    //_player.GetComponent<Player>().TakeDamage(_damage);
}
