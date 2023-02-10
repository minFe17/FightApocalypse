using Unity.VisualScripting;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] float _speed;
    [SerializeField] float _lifeTime;
    [SerializeField] int _damage;

    void Update()
    {
        transform.Translate(Vector3.forward * Time.deltaTime * _speed);
        Invoke("Remove", _lifeTime);
    }

    public void Remove()
    {
        Destroy(this.gameObject);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Map")
            Remove();
        else if(collision.gameObject.tag == "Enemy")
        {
           // collision.gameObject.GetComponent<Enemy>().TakeDamage(_damage);
            Remove();
        }
    }
}
