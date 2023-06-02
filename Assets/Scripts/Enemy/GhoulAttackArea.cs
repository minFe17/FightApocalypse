using UnityEngine;

public class GhoulAttackArea : MonoBehaviour
{
    [SerializeField] Ghoul _ghoul;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
            other.GetComponent<Player>().TakeDamage(_ghoul.Damage);
    }
}
