using UnityEngine;

public class DialogueTrigger : TriggerForInteraction // наследование!
{
    [SerializeField] private TextsToDisplay _textToDisplay;

    void Update()
    {
        // Проверяем, находится ли игрок в зоне и нажата ли клавиша E
        if (ghostInZone && Input.GetKeyDown(KeyCode.E))
        {
            StartDialogue();
        }
    }

    // Метод для начала диалога
    private void StartDialogue()
    {
        Debug.Log("Начинается диалог...");
        
        DialogueController.Instance.ActivateAndSetDialogue(true, _textToDisplay);
    }
}
