using UnityEngine;
using Utils;

public class Explsion : MonoBehaviour
{
    [SerializeField] GameObject _base;
    ParticleSystem _triggerParticle;
    Player _player;

    bool _isTrigger;

    void Awake()
    {
        _triggerParticle = GetComponent<ParticleSystem>();
        _player = GenericSingleton<WaveManager>.Instance.Player;
        _triggerParticle.trigger.AddCollider(_player.transform);    // Trigger ��⿡ �÷��̾� �߰�
    }

    private void OnParticleTrigger()    // Trigger�Ǹ� ȣ���
    {
        if (!_isTrigger)    // �ѹ��� Trigger üũ
        {
            _player.TakeDamage(10);
            _isTrigger = true;
        }
    }

    private void OnParticleSystemStopped()
    {
        Destroy(_base.gameObject);
    }
}
