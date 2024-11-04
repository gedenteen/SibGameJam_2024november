using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForceDiologueTrigger : TriggerForInteraction
{
    [SerializeField] private TextsToDisplay _textToDisplay;
    private bool isPlayed = false;
    void Update()
    {
        // Проверяем, находится ли игрок в зоне и нажата ли клавиша E
        if (_ghostInZone && !isPlayed)
        {
            Debug.Log("Force !!!!!!!!");
            StartDialogue();
            isPlayed = true;
            Destroy(GameObject.Find("TriggerDialogueAfterRelease"));
        }
    }

    // Метод для начала диалога
    private void StartDialogue()
    {
        Debug.Log("Начинается диалог...");

        DialogueController.Instance.ActivateAndSetDialogue(true, _textToDisplay);
    }
    
}
