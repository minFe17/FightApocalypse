using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wall : MonoBehaviour
{
    [SerializeField] GameObject _wall;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
            _wall.SetActive(true);
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.gameObject.tag == "Player")
            _wall.SetActive(false);
    }
}
