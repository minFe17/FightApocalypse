using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    [SerializeField] List<AudioSource> _fbxAudio;
    int _index;

    public void PlayFBXAudio(AudioClip audio)
    {
        _fbxAudio[_index].clip = audio;
        _index++;
        if(_index == _fbxAudio.Count)
            _index = 0;
    }
}
