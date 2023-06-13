using UnityEngine;

public class PoisonBall : MonoBehaviour
{
    [SerializeField] float _speed;
    [SerializeField] float _lifeTime;
    [SerializeField] int _damage;

    public int Damage { get { return _damage; } }

    Transform _bulletPos;

    bool _isThrown;

    void Start()
    {
        Invoke("Remove", _lifeTime);
    }

    public void Init(Transform bulletPos)
    {
        _bulletPos = bulletPos;
    }

    void Update()
    {
        Move();
    }

    void Move()
    {
        if (_bulletPos != null && !_isThrown)
            transform.position = _bulletPos.transform.position;
        if (_isThrown)
            transform.Translate(Vector3.forward * Time.deltaTime * _speed);
    }

    public void ThrownBullet()
    {
        _isThrown = true;
        transform.rotation = _bulletPos.transform.rotation;
    }

    public void Remove()
    {
        Destroy(this.gameObject);
    }
}
