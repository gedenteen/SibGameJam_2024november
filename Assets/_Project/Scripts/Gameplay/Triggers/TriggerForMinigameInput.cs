using System.Collections;
using System.Collections.Generic;
using Cysharp.Threading.Tasks.Triggers;
using UnityEngine;
using UnityEngine.Events;

public class TriggerForMinigameInput : TriggerForInteraction //наследование!
{
    [Header("Refs")]
    [SerializeField] private CanvasForGhost _canvasForGhost;

    [Header("Params")]
    [SerializeField] private string _keySequence;
    [SerializeField] private float _time;
    [SerializeField] private List<UnityEvent> _eventsToInvoke;

    private void Awake()
    {
        base.Awake();
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
        GlobalEvents.EventStartMinigameInput.Invoke();
        _isPlayerCanInteract = false;
        EventGhostInZone?.Invoke(false);
        _canvasForGhost.StartMinigameInput(_keySequence, _time, _eventsToInvoke);
    }

    private void OnEndMinigame()
    {   
        _isPlayerCanInteract = true;
    }
}
