using UnityEngine;

public class MainCamera : MonoBehaviour
{
    [SerializeField] float _offestY;
    [SerializeField] float _offsetZ;
    [SerializeField] Transform _dummy;
    Transform _target;
    public Transform Target { set { _dummy = value; } }

    void Update()
    {
        transform.position = new Vector3(_dummy.position.x, _dummy.position.y + _offestY, _dummy.position.z + _offsetZ);
    }
}
