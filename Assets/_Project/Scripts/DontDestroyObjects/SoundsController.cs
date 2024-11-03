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

    // private void Start()
    // {
    //     Test();
    // }

    // private async UniTask Test()
    // {
    //     for (int i = 0; i < 10; i++)
    //     {
    //         await UniTask.WaitForSeconds(0.5f);
    //         PlayBlip();
    //     }
    // }

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
}
