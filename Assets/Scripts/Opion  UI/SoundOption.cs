using UnityEngine;
using UnityEngine.UI;
using Utils;

public class SoundOption : MonoBehaviour
{
    [SerializeField] Slider _bgmSlider;
    [SerializeField] Slider _sfxSlider;

    SoundManager _soundManager;
    SoundController _soundController;

    private void Awake()
    {
        _soundManager = GenericSingleton<SoundManager>.Instance;
        _soundController = _soundManager.SoundController;

        _bgmSlider.value = _soundManager.BgmSound;
        _sfxSlider.value = _soundManager.SFXSound;

        _soundController.BGM.volume = _soundManager.BgmSound;
        for (int i = 0; i < _soundController.SFXAudio.Count; i++)
            _soundController.SFXAudio[i].volume = _soundManager.SFXSound;
    }

    public void ChangeBGMSoundVolume()
    {
        _soundManager.BgmSound = _bgmSlider.value;
        _soundController.BGM.volume = _bgmSlider.value;
    }

    public void ChangeSFXSoundVolume()
    {
        _soundManager.SFXSound = _sfxSlider.value;
        for (int i = 0; i < _soundController.SFXAudio.Count; i++)
        {
            _soundController.SFXAudio[i].volume = _sfxSlider.value;
        }
    }
}
