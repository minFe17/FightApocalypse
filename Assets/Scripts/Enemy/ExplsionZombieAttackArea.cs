using UnityEngine;

public class ExplsionZombieAttackArea : MonoBehaviour
{
    [SerializeField] ExplsionZombie _explsionZombie;
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
            other.GetComponent<Player>().TakeDamage(_explsionZombie.Damage);
    }
}
