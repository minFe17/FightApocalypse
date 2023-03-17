using UnityEngine;

public class BossAttackArea : MonoBehaviour
{
    [SerializeField] Boss _boss;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            switch (_boss._attackType)
            {
                case EAttackType.RightSlice:
                    other.GetComponent<Player>().TakeDamage(_boss._damage);
                    break;
                case EAttackType.BothHands:
                    other.GetComponent<Player>().TakeDamage(_boss._damage * 2);
                    break;
            }
        }
    }
}
