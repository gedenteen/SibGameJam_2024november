using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TriggerForInteraction : MonoBehaviour
{
    public static UnityEvent<bool> EventGhostInZone = new UnityEvent<bool>();

    [SerializeField] private SpriteRenderer _mySpriteRenderer;
    [SerializeField] private bool _hideSpriteInGame = true;

    // Переменная для отслеживания, находится ли игрок в зоне
    protected bool _ghostInZone = false;
    protected bool _isPlayerCanInteract = true;

    protected void Awake()
    {
        if (_hideSpriteInGame)
        {
            _mySpriteRenderer.enabled = false;
        }
    }

    protected void OnTriggerEnter2D(Collider2D other)
    {
        if (!_isPlayerCanInteract)
        {
            return;
        }
    
        Debug.Log($"TriggerForInteraction: OnTriggerEnter2D: my name = {gameObject.name}");
        GhostController ghostController;
        bool itIsGhost = other.TryGetComponent<GhostController>(out ghostController);
        
        if (itIsGhost)
        {
            _ghostInZone = true;
            EventGhostInZone?.Invoke(true);
        }

    }

    protected void OnTriggerExit2D(Collider2D other)
    {
        //Debug.Log("TriggerForInteraction: OnTriggerExit2D");
        _ghostInZone = false;
        EventGhostInZone?.Invoke(false);
    }
}
