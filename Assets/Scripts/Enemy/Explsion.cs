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
        _triggerParticle.trigger.AddCollider(_player.transform);    // Trigger 모듈에 플레이어 추가
    }

    private void OnParticleTrigger()    // Trigger되면 호출됨
    {
        if (!_isTrigger)    // 한번만 Trigger 체크
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
