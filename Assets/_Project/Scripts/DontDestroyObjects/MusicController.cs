using System;
using System.Collections;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;

public class MusicController : MonoBehaviour
{
    [Header("Params")]
    [SerializeField] private byte _currentAudioSource = 1;
    [SerializeField] private float _changeVolumeMultiplier = 1;

    [Header("References")]
    [SerializeField] private AudioSource _myAudioSource1;
    [SerializeField] private AudioSource _myAudioSource2;
    [SerializeField] private AudioClip _trackTest;
    [SerializeField] private AudioClip _trackMainMenu;
    [SerializeField] private AudioClip _trackLevelSwamp;

    private void Awake()
    {
        _currentAudioSource = 1;
        SmoothChangeTrack(SceneNames.MainMenu, true).Forget();

        GlobalEvents.EventStartSceneLoading.AddListener(OnSceneStartLoading);
        GlobalEvents.EventEndSceneLoading.AddListener(OnSceneEndLoading);
    }

    private void OnSceneStartLoading(int sceneIndex)
    {
        SceneNames sceneName = (SceneNames)sceneIndex;
        SmoothChangeTrack(sceneName, false).Forget();
    }

    private void OnSceneEndLoading(int sceneIndex)
    {
        SceneNames sceneName = (SceneNames)sceneIndex;
        SmoothChangeTrack(sceneName, true).Forget();
    }

    private async UniTask SmoothChangeTrack(SceneNames sceneName, bool turnOn)
    {
        if (turnOn)
        {
            AudioClip trackToPlay;

            switch (sceneName)
            {
                case SceneNames.MainMenu:
                    trackToPlay = _trackMainMenu;
                    break;
                case SceneNames.Level1_Swamp:
                    trackToPlay = _trackLevelSwamp;
                    break;
                default:
                    trackToPlay = _trackMainMenu;
                    break;
            }

            Debug.Log($"MusicController: SmoothChangeTrack: sceneName={sceneName} trackToPlay={trackToPlay}");

            if (_currentAudioSource == 1)
            {
                _currentAudioSource = 2;
                await SmoothTurnOnAudioSource(_myAudioSource2, trackToPlay);
            }
            else if (_currentAudioSource == 2)
            {
                _currentAudioSource = 1;
                await SmoothTurnOnAudioSource(_myAudioSource1, trackToPlay);
            }
            else
            {
                Debug.LogError($"MusicController: SmoothChangeTrack: unexpected _currentAudioSource={_currentAudioSource}");
                return;
            }
        }
        else
        {
            if (_currentAudioSource == 1)
            {
                await SmoothTurnOffAudioSource(_myAudioSource1);
            }
            else if (_currentAudioSource == 2)
            {
                await SmoothTurnOffAudioSource(_myAudioSource2);
            }
            else
            {
                Debug.LogError($"MusicController: SmoothChangeTrack: unexpected _currentAudioSource={_currentAudioSource}");
                return;
            }
        }
    }

    private async UniTask SmoothTurnOnAudioSource(AudioSource audioSource, AudioClip audioClip)
    {
        audioSource.clip = audioClip;
        audioSource.volume = 0f;
        audioSource.Play();

        while (audioSource.volume < 1f)
        {
            audioSource.volume = Mathf.MoveTowards(audioSource.volume, 1f, Time.deltaTime * _changeVolumeMultiplier);
            //Debug.Log($"MusicController: SmoothChangeTrack: audioSource.volume={audioSource.volume}");
            await UniTask.Yield();
        }
    }

    private async UniTask SmoothTurnOffAudioSource(AudioSource audioSource)
    {
        while (audioSource.volume > 0f)
        {
            audioSource.volume = Mathf.MoveTowards(audioSource.volume, 0f, Time.deltaTime * _changeVolumeMultiplier);
            await UniTask.Yield();
        }

        audioSource.Stop();
    }
}
