using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerForMinigameInput : TriggerForInteraction //наследование!
{
    [SerializeField] private string _keySequence;
    [SerializeField] private float _time;

    private void Awake()
    {
        GlobalEvents.EventEndMinigameInput.AddListener(OnEndMinigame);
    }

    private void OnDestroy()
    {
        GlobalEvents.EventEndMinigameInput.RemoveListener(OnEndMinigame);
    }

    void Update()
    {
        // Проверяем, находится ли игрок в зоне и нажата ли клавиша E
        if (_isPlayerCanInteract && _ghostInZone && Input.GetKeyDown(KeyCode.E))
        {
            StartMinigame();
        }
    }

    // Метод для начала диалога
    private void StartMinigame()
    {
        GlobalEvents.EventStartMinigameInput.Invoke(_keySequence, _time);
        _isPlayerCanInteract = false;
        EventGhostInZone?.Invoke(false);
    }

    private void OnEndMinigame()
    {   
        _isPlayerCanInteract = true;
    }
}
