using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoShowDialog : MonoBehaviour
{
    [SerializeField] private TextsToDisplay _dialogue;

    private void Start()
    {
        DialogueController.Instance.ActivateAndSetDialogue(true, _dialogue);
    } 
}
