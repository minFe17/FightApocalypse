using UnityEngine;
using Utils;

public class MapCamera : MonoBehaviour
{
    [SerializeField] private bool x, y, z;

    Transform _target;

    void Start()
    {
        _target = GenericSingleton<WaveManager>.Instance.Player.transform;
    }

    void Update()
    {
        if (_target == false) return;
        transform.position = new Vector3(
            (x ? _target.position.x : transform.position.x),
            (y ? _target.position.y : transform.position.y),
            (z ? _target.position.z : transform.position.z));
    }
}
