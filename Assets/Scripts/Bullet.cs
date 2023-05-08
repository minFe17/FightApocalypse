using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] float _speed;
    [SerializeField] float _lifeTime;
    [SerializeField] int _damage;

    void Start()
    {
        Invoke("Remove", _lifeTime);
    }

    void Update()
    {
        transform.Translate(Vector3.forward * Time.deltaTime * _speed);
    }

    public void Remove()
    {
        Destroy(this.gameObject);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            collision.gameObject.GetComponent<Enemy>().TakeDamage(_damage);
        }
        Remove();
    }
}
