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

    private void Awake()
    {
        Instance = this;
    }
}
