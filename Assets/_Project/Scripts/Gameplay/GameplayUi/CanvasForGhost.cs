using System.Collections;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class CanvasForGhost : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private List<ImageOfKey> _imagesOfKey = new List<ImageOfKey>();

    [Header("Private fields")]
    [SerializeField] private bool _isMinigameInput = false;
    [SerializeField] private string _keySequence = "";
    [SerializeField] private int _keySequenceIndex = 0;

    private void Awake()
    {
        GlobalEvents.EventStartMinigameInput.AddListener(StartMinigameInput);
    }

    private void OnDestroy()
    {
        GlobalEvents.EventStartMinigameInput.RemoveListener(StartMinigameInput);
    }

    private void Update()
    {
        if (_isMinigameInput)
        {
            // Проверка, была ли нажата любая клавиша
            if (Input.anyKeyDown)
            {
                string pressedKeys = Input.inputString;
                
                if (!string.IsNullOrEmpty(pressedKeys))
                {
                    UpdateProgressInMinigame(pressedKeys);
                }
            }
        }
    }

    private void StartMinigameInput(string keySequence)
    {
        StartMinigameInputAsync(keySequence);
    }

    private async void StartMinigameInputAsync(string keySequence)
    {
        // надо подждать 1 кадр, иначе в Update получим нажатую E, которую нажимали для триггера
        await UniTask.Yield(); 

        for (int i = 0; i < keySequence.Length; i++)
        {
            _imagesOfKey[i].Image.color = Color.white;
            _imagesOfKey[i].TextMesh.text = keySequence.Substring(i, 1).ToUpper();
            _imagesOfKey[i].gameObject.SetActive(true);
        }

        _keySequence = keySequence;
        _keySequenceIndex = 0;
        _isMinigameInput = true;
    }

    private void StopMinigameInput()
    {
        for (int i = 0; i < _keySequence.Length; i++)
        {
            _imagesOfKey[i].gameObject.SetActive(false);
        }

        _keySequence = "";
        _keySequenceIndex = 0;
        _isMinigameInput = false;

        GlobalEvents.EventEndMinigameInput.Invoke();
    }

    private void UpdateProgressInMinigame(string pressedKeys)
    {
        Debug.Log("Pressed keys: " + pressedKeys);
        for (int i = 0; i < pressedKeys.Length; i++)
        {
            if (pressedKeys[i] == _keySequence[_keySequenceIndex])
            {
                _imagesOfKey[_keySequenceIndex].Image.color = Color.green;
                _keySequenceIndex++;

                if (_keySequenceIndex == _keySequence.Length)
                {
                    StopMinigameInput();
                    return;
                }
            }
            else
            {
                StopMinigameInput();
                return;
            }
        }
    }
}
