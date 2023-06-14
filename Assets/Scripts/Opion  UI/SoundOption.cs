using UnityEngine;
using UnityEngine.UI;
using Utils;

public class SoundOption : MonoBehaviour
{
    [SerializeField] Slider _bgmSlider;
    [SerializeField] Slider _sfxSlider;

    SoundController _soundController;

    private void Awake()
    {
        _soundController = GenericSingleton<SoundManager>.Instance.SoundController;
        _bgmSlider.value = 0.5f;
        _sfxSlider.value = 0.5f;
        _soundController.BGM.volume = 0.5f;
        for (int i = 0; i < _soundController.SFXAudio.Count; i++)
            _soundController.SFXAudio[i].volume = 0.5f;
    }

    public void ChangeBGMSoundVolume()
    {
        _soundController.BGM.volume = _bgmSlider.value;
    }

    public void ChangeSFXSoundVolume()
    {
        for (int i = 0; i < _soundController.SFXAudio.Count; i++)
        {
            _soundController.SFXAudio[i].volume = _sfxSlider.value;
        }
    }
}
