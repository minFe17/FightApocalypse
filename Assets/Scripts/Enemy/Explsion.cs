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

        // 파티클 트리거 모듈에 플레이어 Collider 등록
        _triggerParticle.trigger.AddCollider(_player.transform);
    }

    // 파티클이 트리거 충돌 시 호출됨
    private void OnParticleTrigger()    
    {
        // 한 번만 피해 적용
        if (!_isTrigger)                
        {
            _player.TakeDamage(_damage);     
            _isTrigger = true;
        }
    }

    private void OnParticleSystemStopped()   // 파티클이 모두 재생되고 종료되었을 때 호출됨
    {
        Destroy(_base.gameObject);           // 이펙트 루트 오브젝트 제거
    }
}
