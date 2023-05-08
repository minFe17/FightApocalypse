using UnityEngine;

public class RaptorAttackArea : MonoBehaviour
{
    [SerializeField] Raptor _raptor;

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Player"))
            other.GetComponent<Player>().TakeDamage(_raptor.Damage);

    }
}
