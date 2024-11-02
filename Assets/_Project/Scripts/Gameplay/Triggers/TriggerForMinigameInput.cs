using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerForMinigameInput : TriggerForInteraction //наследование!
{
    [SerializeField] private string _keySequence;

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
        if (isPlayerCanInteract && ghostInZone && Input.GetKeyDown(KeyCode.E))
        {
            StartMinigame();
        }
    }

    // Метод для начала диалога
    private void StartMinigame()
    {
        GlobalEvents.EventStartMinigameInput.Invoke(_keySequence);
        isPlayerCanInteract = false;
        EventGhostInZone?.Invoke(false);
    }

    private void OnEndMinigame()
    {   
        isPlayerCanInteract = true;
    }
}
