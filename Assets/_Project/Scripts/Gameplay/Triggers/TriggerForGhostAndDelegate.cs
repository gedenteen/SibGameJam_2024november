using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.PlayerLoop;

public class TriggerForGhostAndDelegate : TriggerForInteraction
{
    [SerializeField] private List<UnityEvent> _eventsForInvoke;
    [SerializeField] private bool _onlyOneInteraction = true;

    void Update()
    {
        // Проверяем, находится ли игрок в зоне и нажата ли клавиша E
        if (_isPlayerCanInteract && _ghostInZone && Input.GetKeyDown(KeyCode.E))
        {
            // Вызываем эвенты
            for (int i = 0; i < _eventsForInvoke.Count; i++)
            {
                if (_eventsForInvoke[i] != null)
                {
                    _eventsForInvoke[i].Invoke();
                }
                else
                {
                    Debug.LogError($"TriggerForGhostAndDelegate: event[{i}] is null");
                }
            }

            if (_onlyOneInteraction)
            {
                EventGhostInZone?.Invoke(false);
                _isPlayerCanInteract = false;
            }
        }
    }
}
