using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicController : MonoBehaviour
{
    [SerializeField] private AudioSource _myAudioSource;
    [SerializeField] private AudioClip _trackTest;

    private void Start()
    {
        _myAudioSource.clip = _trackTest;
        _myAudioSource.Play();
    }
}
