using UnityEngine;
using Utils;

public class PoisonBallEffect : MonoBehaviour
{
    [SerializeField] PoisonBall _base;
    
    ParticleSystem _triggerParticle;
    Player _player;

    bool _isTrigger;

    void Awake()
    {
        _triggerParticle = GetComponent<ParticleSystem>();
        _player = GenericSingleton<WaveManager>.Instance.Player;
        _triggerParticle.trigger.AddCollider(_player.transform);
    }

    private void OnParticleTrigger()
    {
        if (!_isTrigger)
        {
            _player.TakeDamage(_base.Damage);
            _isTrigger = true;
        }
    }
}
