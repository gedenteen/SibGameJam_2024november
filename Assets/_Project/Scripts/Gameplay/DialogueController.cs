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
        if (textsToDisplay.spriteChar1 != null)
        {
            _imageForCharLeft.gameObject.SetActive(true);
            _imageForCharLeft.sprite = textsToDisplay.spriteChar1;
        }
        else
        {
            _imageForCharLeft.gameObject.SetActive(false);
        }

        if (textsToDisplay.spriteChar2 != null)
        {
            _imageForCharRight.gameObject.SetActive(true);
            _imageForCharRight.sprite = textsToDisplay.spriteChar2;
        }
        else
        {
            _imageForCharRight.gameObject.SetActive(false);
        }

        _printingText.textsToDisplay = textsToDisplay;
        Activate(activate, _delayForActivate);
    }

    private void OnDialogueOver()
    {
        Activate(false, _delayForActivate);
    }
}
