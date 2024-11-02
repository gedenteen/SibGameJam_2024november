using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicController : MonoBehaviour
{
    [SerializeField] private AudioSource _myAudioSource;
    [SerializeField] private AudioClip _trackTest;
    [SerializeField] private AudioClip _trackMainMenu;

    private void Awake()
    {
        OnMainMenuLoad();
        GlobalEvents.EventMainMenuLoaded.AddListener(OnMainMenuLoad);
    }

    private void OnMainMenuLoad()
    {
        _myAudioSource.clip = _trackMainMenu;
        _myAudioSource.Play();
    }
}
