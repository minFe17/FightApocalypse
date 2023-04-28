using UnityEngine;

public class MainCamera : MonoBehaviour
{
    [SerializeField] float _offestY;
    [SerializeField] float _offsetZ;

    Transform _target;
    public Transform Target { set { _target = value; } }

    void Update()
    {
        transform.position = new Vector3(_target.position.x, _target.position.y + _offestY, _target.position.z + _offsetZ);
    }
}
