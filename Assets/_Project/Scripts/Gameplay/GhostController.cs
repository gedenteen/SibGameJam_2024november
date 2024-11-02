using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class GhostController : MonoBehaviour
{
    [Header("Parameters")]
    [SerializeField] private float _movementSpeed;

    [Header("References")]
    [SerializeField] private SpriteRenderer _mySpriteButtonE;

    private void Awake()
    {
        TriggerForInteraction.EventGhostInZone.AddListener(OnTriggerForIntecation);
    }

    private void Update()
    {
        Vector3 input = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        transform.position += input * _movementSpeed * Time.deltaTime;
    }

    private void OnDestroy()
    {
        TriggerForInteraction.EventGhostInZone.RemoveListener(OnTriggerForIntecation);
    }

    private void OnTriggerForIntecation(bool entered)
    {
        _mySpriteButtonE.gameObject.SetActive(entered);
    }
}
