using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DialogueController : UiPanel
{
    public static DialogueController Instance { get; private set;}

    [Header("References to objects")]
    [SerializeField] private PrintingText _printingText;
    [SerializeField] private TextMeshProUGUI _mainText;

    [Header("Parameters")]
    [SerializeField] private float _delayForActivate = 0.3f;

    private void Awake()
    {
        Instance = this;
        _printingText.eventDialogueIsOver.AddListener(OnDialogueOver);
    }

    public void ActivateAndSetDialogue(bool activate, TextsToDisplay textsToDisplay)
    {
        _printingText.textsToDisplay = textsToDisplay;
        Activate(activate, _delayForActivate);
    }

    private void OnDialogueOver()
    {
        Activate(false, _delayForActivate);
    }
}
