using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ForceDialogueTriggerForIvan : MonoBehaviour
{
    [SerializeField] private SpriteRenderer _mySpriteRenderer;
    [SerializeField] private bool _hideSpriteInGame = true;
    [SerializeField] private List<UnityEvent> _eventsForInvoke;
    [SerializeField] private TextsToDisplay _textToDisplay;
    private bool isPlayed = false;

    private void Awake()
    {
        if (_hideSpriteInGame)
        {
            _mySpriteRenderer.gameObject.SetActive(false);
        }
    }

    protected void OnTriggerEnter2D(Collider2D other)
    {   
        IvanController ivanController;
        bool itIsIvan = other.TryGetComponent<IvanController>(out ivanController);
        Debug.Log($"TriggerForIvan: OnTriggerEnter2D: itIsIvan={itIsIvan}");

        if (itIsIvan)
        {
            Debug.Log("Vanyaaaaa");
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
            StartDialogue();
            isPlayed = true;
        }
    }

    private void StartDialogue()
    {
        Debug.Log("Начинается диалог...");

        DialogueController.Instance.ActivateAndSetDialogue(true, _textToDisplay);
    }
}
