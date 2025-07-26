using UnityEngine;
using Utils;

public class Explsion : MonoBehaviour
{
    [SerializeField] GameObject _base;       
    ParticleSystem _triggerParticle;         
    Player _player;                          

    int _damage = 10;
    bool _isTrigger;                       

    void Awake()
    {
        _triggerParticle = GetComponent<ParticleSystem>();
        _player = GenericSingleton<WaveManager>.Instance.Player;

        // ��ƼŬ Ʈ���� ��⿡ �÷��̾� Collider ���
        _triggerParticle.trigger.AddCollider(_player.transform);
    }

    // ��ƼŬ�� Ʈ���� �浹 �� ȣ���
    private void OnParticleTrigger()    
    {
        // �� ���� ���� ����
        if (!_isTrigger)                
        {
            _player.TakeDamage(_damage);     
            _isTrigger = true;
        }
    }

    private void OnParticleSystemStopped()   // ��ƼŬ�� ��� ����ǰ� ����Ǿ��� �� ȣ���
    {
        Destroy(_base.gameObject);           // ����Ʈ ��Ʈ ������Ʈ ����
    }
}
