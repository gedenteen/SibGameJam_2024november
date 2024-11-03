using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class GhostController : MonoBehaviour
{
    [Header("Parameters")]
    [SerializeField] private float _movementSpeed;
    [SerializeField] private bool _canMove = true;

    [Header("References")]
    [SerializeField] private SpriteRenderer _characterSprite;
    [SerializeField] private CanvasForGhost _canvasForGhost;

    private void Awake()
    {
        TriggerForInteraction.EventGhostInZone.AddListener(OnTriggerForIntecation);
        GlobalEvents.EventStartMinigameInput.AddListener(OnStartMinigameInput);
        GlobalEvents.EventEndMinigameInput.AddListener(OnEndMinigameInput);
    }

    private void Update()
    {
        if (!_canMove)
        {
            return;
        }

        Vector3 input = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        transform.position += input * _movementSpeed * Time.deltaTime;

        if (input.x != 0)
        {
            _characterSprite.flipX = input.x > 0f ? false : true;
        }
    }

    private void OnDestroy()
    {
        TriggerForInteraction.EventGhostInZone.RemoveListener(OnTriggerForIntecation);
        GlobalEvents.EventStartMinigameInput.RemoveListener(OnStartMinigameInput);
        GlobalEvents.EventEndMinigameInput.RemoveListener(OnEndMinigameInput);
    }

    private void OnStartMinigameInput(string keySequence, float time)
    {
        _canMove = false;
    }

    private void OnEndMinigameInput()
    {
        _canMove = true;
    }

    private void OnTriggerForIntecation(bool entered)
    {
        _canvasForGhost.ShowButtonForInteraction(entered);
    }
}
