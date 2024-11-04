using System.Collections;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.PlayerLoop;

public class CanvasForGhost : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private ImageOfKey _imageOfKeyForInteraction;
    [SerializeField] private List<ImageOfKey> _imagesOfKey = new List<ImageOfKey>();
    [SerializeField] private TextMeshProUGUI _textForTimer;
    
    [Header("Colors")]
    [SerializeField] private Color _colorForUnpressedKey;
    [SerializeField] private Color _colorForPressedKey;

    [Header("Private fields")]
    [SerializeField] private bool _isMinigameInput = false;
    [SerializeField] private string _keySequence = "";
    [SerializeField] private int _keySequenceIndex = 0;
    [SerializeField] private float _timer = 0;
    [SerializeField] private List<UnityEvent> _eventsToInvoke;

    private void Update()
    {
        if (_isMinigameInput)
        {
            _timer -= Time.deltaTime;
            if (_timer <= 0f)
            {
                StopMinigameInput();
            }
            _textForTimer.text = _timer.ToString("F1");

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

    public void ShowButtonForInteraction(bool activatate)
    {
        _imageOfKeyForInteraction.gameObject.SetActive(activatate);
    }

    public void StartMinigameInput(string keySequence, float time, List<UnityEvent> eventsToInvoke)
    {
        StartMinigameInputAsync(keySequence, time, eventsToInvoke);
    }

    private async void StartMinigameInputAsync(string keySequence, float time, List<UnityEvent> eventsToInvoke)
    {
        // надо подждать 1 кадр, иначе в Update получим нажатую E, которую нажимали для триггера
        await UniTask.Yield();

        _eventsToInvoke = eventsToInvoke;

        _timer = time;
        _textForTimer.text = time.ToString("F1");
        _textForTimer.gameObject.SetActive(true);

        for (int i = 0; i < keySequence.Length; i++)
        {
            _imagesOfKey[i].Image.color = _colorForUnpressedKey;
            _imagesOfKey[i].TextMesh.text = keySequence.Substring(i, 1).ToUpper();
            _imagesOfKey[i].gameObject.SetActive(true);
        }

        if (SoundsController.Instance != null)
        {
            SoundsController.Instance.PlayTimer();
        }

        _keySequence = keySequence;
        _keySequenceIndex = 0;
        _isMinigameInput = true;
    }

    private void StopMinigameInput()
    {
        _textForTimer.text = "---";
        _textForTimer.gameObject.SetActive(false);

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
            if (char.ToLower(pressedKeys[i]) == char.ToLower(_keySequence[_keySequenceIndex]))
            {
                _imagesOfKey[_keySequenceIndex].Image.color = _colorForPressedKey;
                _keySequenceIndex++;

                if (SoundsController.Instance != null)
                {
                    SoundsController.Instance.PlayPositiveClick();
                }

                if (_keySequenceIndex == _keySequence.Length)
                {
                    // Успешное заверешение мини-игры
                    for (int j = 0; j < _eventsToInvoke.Count; j++)
                    {
                        _eventsToInvoke[j].Invoke();
                    }
                    StopMinigameInput();
                    return;
                }
            }
            else
            {
                if (SoundsController.Instance != null)
                    SoundsController.Instance.PlayNegativeClick();
                StopMinigameInput();
                return;
            }
        }
    }
}
