using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DialogueController : UiPanel
{
    public static DialogueController Instance { get; private set;}

    [Header("References to objects")]
    [SerializeField] private PrintingText _printingText;
    [SerializeField] private TextMeshProUGUI _mainText;
    [SerializeField] private Image _imageForCharLeft;
    [SerializeField] private Image _imageForCharRight;

    [Header("Parameters")]
    [SerializeField] private float _delayForActivate = 0.3f;

    private void Awake()
    {
        Instance = this;
        _printingText.eventDialogueIsOver.AddListener(OnDialogueOver);
    }

    public void ActivateAndSetDialogue(bool activate, TextsToDisplay textsToDisplay)
    {
        _imageForCharLeft.sprite = textsToDisplay.spriteChar1;
        _imageForCharRight.sprite = textsToDisplay.spriteChar2;
        _printingText.textsToDisplay = textsToDisplay;
        Activate(activate, _delayForActivate);
    }

    private void OnDialogueOver()
    {
        Activate(false, _delayForActivate);
    }
}
