using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCamera : MonoBehaviour
{
    [SerializeField] Transform _cameraPos;

    void Start()
    {
        
    }

    void Update()
    {
        transform.position = _cameraPos.position;
    }
}
