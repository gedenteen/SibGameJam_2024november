using System.Collections;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;

public class SoundsController : MonoBehaviour
{
    public static SoundsController Instance {get; private set;}

    [SerializeField] private List<AudioSource> _audioSources;

    [Header("AudioClips")]
    [SerializeField] private AudioClip _clipBlip;
    [SerializeField] private AudioClip _clipPositivieClick;
    [SerializeField] private AudioClip _clipNegativeClick;
    [SerializeField] private AudioClip _clipTimer;

    [Header("Private fields")]
    [SerializeField] private int _currentAudioSource;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

        _currentAudioSource = 0;
    }

    private void PlayAudioClip(AudioClip audioClip)
    {
        _audioSources[_currentAudioSource].clip = audioClip;
        _audioSources[_currentAudioSource].Play();
        
        _currentAudioSource++;
        if (_currentAudioSource >= _audioSources.Count)
        {
            _currentAudioSource = 0;
        }
    }

    public void PlayBlip()
    {
        PlayAudioClip(_clipBlip);
    }

    public void PlayTimer()
    {
        PlayAudioClip(_clipTimer);
    }

    public void PlayPositiveClick()
    {
        PlayAudioClip(_clipPositivieClick);
    }

    public void PlayNegativeClick()
    {
        PlayAudioClip(_clipNegativeClick);
    }
}
