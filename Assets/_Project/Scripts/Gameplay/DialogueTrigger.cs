using UnityEngine;

public class DialogueTrigger : MonoBehaviour
{
    // Переменная для отслеживания, находится ли игрок в зоне
    private bool playerInZone = false;

    void Update()
    {
        // Проверяем, находится ли игрок в зоне и нажата ли клавиша E
        if (playerInZone && Input.GetKeyDown(KeyCode.E))
        {
            StartDialogue();
        }
    }

    // Метод для начала диалога
    private void StartDialogue()
    {
        Debug.Log("Начинается диалог...");
        // Здесь можно вызвать ваш метод для показа диалога.
        // Например: DialogueManager.Instance.StartDialogue(dialogueData);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("DialogueTrigger: OnTriggerEnter2D");
        playerInZone = true;
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        Debug.Log("DialogueTrigger: OnTriggerExit2D");
        playerInZone = false;
    }
}
