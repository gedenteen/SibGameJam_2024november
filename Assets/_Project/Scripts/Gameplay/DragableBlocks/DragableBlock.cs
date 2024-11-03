using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragableBlock : TriggerForInteraction // наследование!
{
    [Header("Refs")]
    [SerializeField] private GhostController _ghostContoller;
    [SerializeField] private Rigidbody2D _rigidbody2DOfCollider;

    [Header("Private fields")]
    [SerializeField] private bool _isFollow = false;
    [SerializeField] private Vector3 _startOffset = Vector3.zero;
    
    private void Awake()
    {
        _ghostInZone = false;
        _isFollow = false;
    }

    private void Update()
    {
        if (_isFollow)
        {
            _rigidbody2DOfCollider.transform.position = _ghostContoller.transform.position + _startOffset;
        }

        if (!_isPlayerCanInteract)
        {
            return;
        }

        // Проверяем, находится ли игрок в зоне и нажата ли клавиша E
        if (_ghostInZone && Input.GetKeyDown(KeyCode.E))
        {
            if (_isFollow)
            {
                StopFollow();
            }
            else
            {
                StartFollow();
            }
        }
    }

    private void StartFollow()
    {
        _startOffset = _rigidbody2DOfCollider.transform.position - _ghostContoller.transform.position;
        _rigidbody2DOfCollider.gravityScale = 0f;
        _isFollow = true;
    }

    private void StopFollow()
    {
        _rigidbody2DOfCollider.gravityScale = 1f;
        _isFollow = false;
    }

    public void MarkAsUninteractable()
    {
        _isPlayerCanInteract = false;
        StopFollow();
        _ghostInZone = false;
        EventGhostInZone?.Invoke(false);
    }
}
