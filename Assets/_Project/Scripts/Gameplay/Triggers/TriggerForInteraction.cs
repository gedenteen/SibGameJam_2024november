using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TriggerForInteraction : MonoBehaviour
{
    public static UnityEvent<bool> EventGhostInZone = new UnityEvent<bool>();

    // Переменная для отслеживания, находится ли игрок в зоне
    protected bool _ghostInZone = false;
    protected bool _isPlayerCanInteract = true;

    protected void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("TriggerForInteraction: OnTriggerEnter2D");
        _ghostInZone = true;
        EventGhostInZone?.Invoke(true);
    }

    protected void OnTriggerExit2D(Collider2D other)
    {
        Debug.Log("TriggerForInteraction: OnTriggerExit2D");
        _ghostInZone = false;
        EventGhostInZone?.Invoke(false);
    }
}
