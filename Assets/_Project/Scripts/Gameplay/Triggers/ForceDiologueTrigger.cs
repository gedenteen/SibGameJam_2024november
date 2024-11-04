using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForceDiologueTrigger : TriggerForInteraction
{
    [SerializeField] private TextsToDisplay _textToDisplay;
    private bool isPlayed = false;
    void Update()
    {
        // ���������, ��������� �� ����� � ���� � ������ �� ������� E
        if (_ghostInZone && !isPlayed)
        {
            Debug.Log("Force !!!!!!!!");
            StartDialogue();
            isPlayed = true;
            Destroy(GameObject.Find("TriggerDialogueAfterRelease"));
        }
    }

    // ����� ��� ������ �������
    private void StartDialogue()
    {
        Debug.Log("���������� ������...");

        DialogueController.Instance.ActivateAndSetDialogue(true, _textToDisplay);
    }
    
}
