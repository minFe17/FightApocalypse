using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCamera : MonoBehaviour
{
    [SerializeField] Transform _target;
    [SerializeField] float _offestY;
    [SerializeField] float _offsetZ;

    void Start()
    {
        
    }

    void Update()
    {
        //ī�޶� �̵�
        transform.position = new Vector3(_target.position.x, _target.position.y + _offestY, _target.position.z + _offsetZ);
    }
}
