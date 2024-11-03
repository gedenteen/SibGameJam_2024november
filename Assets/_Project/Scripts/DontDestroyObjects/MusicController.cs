using System;
using Cysharp.Threading.Tasks;
using UnityEngine;

public class MusicController : MonoBehaviour
{
    public static MusicController Instance { get; private set;}

    [Header("Params")]
    [SerializeField] private byte _currentAudioSource = 1;
    [SerializeField] private float _changeVolumeMultiplier = 1;

    [Header("References")]
    [SerializeField] private AudioSource _myAudioSource1;
    [SerializeField] private AudioSource _myAudioSource2;
    [SerializeField] private AudioClip _trackMainMenu;
    [SerializeField] private AudioClip _trackLevel1_Swamp;
    [SerializeField] private AudioClip _trackLevel2_Forest;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }

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
        AudioSource activeAudioSource = _currentAudioSource == 1 ? _myAudioSource1 : _myAudioSource2;
        AudioSource inactiveAudioSource = _currentAudioSource == 1 ? _myAudioSource2 : _myAudioSource1;

        if (turnOn)
        {
            // Останавливаем текущий трек на активном AudioSource, если он играет
            await SmoothTurnOffAudioSource(activeAudioSource);

            AudioClip trackToPlay = GetAudioClipForScene(sceneName);
            if (trackToPlay == null)
            {
                Debug.LogError($"MusicController: SmoothChangeTrack: No track assigned for scene {sceneName}");
                return;
            }

            Debug.Log($"MusicController: SmoothChangeTrack: sceneName={sceneName} trackToPlay={trackToPlay}");
            await SmoothTurnOnAudioSource(inactiveAudioSource, trackToPlay);

            // Переключаем текущий AudioSource
            _currentAudioSource = _currentAudioSource == 1 ? (byte)2 : (byte)1;
        }
        else
        {
            await SmoothTurnOffAudioSource(activeAudioSource);
        }
    }

    private AudioClip GetAudioClipForScene(SceneNames sceneName)
    {
        return sceneName switch
        {
            SceneNames.MainMenu => _trackMainMenu,
            SceneNames.Level1_Swamp => _trackLevel1_Swamp,
            SceneNames.Level2_Forest => _trackLevel2_Forest,
            _ => _trackMainMenu,
        };
    }

    private async UniTask SmoothTurnOnAudioSource(AudioSource audioSource, AudioClip audioClip)
    {
        audioSource.clip = audioClip;
        audioSource.volume = 0f;
        audioSource.Play();

        while (audioSource.volume < 1f)
        {
            audioSource.volume = Mathf.MoveTowards(audioSource.volume, 1f, Time.deltaTime * _changeVolumeMultiplier);
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
