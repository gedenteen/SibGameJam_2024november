using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Events;

// A class that output text character-by-character with a delay (it can be called as printing effect)
public class PrintingText : MonoBehaviour
{
    [Header("References")]
    public TextsToDisplay textsToDisplay;
    [SerializeField] TextMeshProUGUI textMesh;
    [SerializeField] TextMeshProUGUI textForSpeaketName;
    [SerializeField] Button button;

    public UnityEvent eventDialogueIsOver = new UnityEvent();

    private bool isTyping = false;
    private IEnumerator coroutineForTextAnimation = null;
    private int indexOfText = 0;

    private void Awake()
    {
        button.onClick.AddListener(OnButtonClick);
    }

    private void Start()
    {
        PrintText();
    }

    public void PrintText()
    {
        textMesh.text = "";
        coroutineForTextAnimation = CoroutinePrint(
            textsToDisplay.phrases[indexOfText].nameOfSpeaker,
            textsToDisplay.phrases[indexOfText].text,
            textsToDisplay.delayForWrite);
        StartCoroutine(coroutineForTextAnimation);
    }

    private IEnumerator CoroutinePrint(string speakerName, string text, float delayForWrite = 0.05f)
    {
        textForSpeaketName.text = speakerName;

        WaitForSeconds delay = new WaitForSeconds(delayForWrite);
        
        isTyping = true;
        int i = 0;
        while (i < text.Length)
        {
            textMesh.text += text[i];
            i++;
            yield return delay;
        }
        isTyping = false;
    }

    private void OnButtonClick()
    {
        if (isTyping)
        {
            // Stop animation (coroutine), display a whole text
            if (coroutineForTextAnimation != null)
            {
                StopCoroutine(coroutineForTextAnimation);
                textMesh.text = textsToDisplay.phrases[indexOfText].text;
                isTyping = false;
            }
            else
            {
                Debug.LogError($"PrintingText: OnButtonClick: coroutineForTextAnimation is null");
            }
        }
        else
        {
            // Display next text (if exists)
            indexOfText++;
            if (indexOfText < textsToDisplay.phrases.Count)
            {
                PrintText();
            }
            else
            {
                eventDialogueIsOver?.Invoke();
            }
        }
    }
}
