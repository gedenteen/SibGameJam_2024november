using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TriggerForGhostAndDelegate : TriggerForInteraction
{
    [SerializeField] private UnityEvent _methodForInvoke;
    [SerializeField] private bool _onlyOneInteraction = true;

    void Update()
    {
        // Проверяем, находится ли игрок в зоне и нажата ли клавиша E
        if (_isPlayerCanInteract && _ghostInZone && Input.GetKeyDown(KeyCode.E))
        {
            _methodForInvoke.Invoke();

            if (_onlyOneInteraction)
            {
                EventGhostInZone?.Invoke(false);
                _isPlayerCanInteract = false;
            }
        }
    }
}
