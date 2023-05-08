using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieAttackArea : MonoBehaviour
{
    [SerializeField] Zombie _zombie;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
            other.GetComponent<Player>().TakeDamage(_zombie.Damage);
    }
}
