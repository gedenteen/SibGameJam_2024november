using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "TextsToDisplay", menuName = "ScriptableObjects/Create TextsToDisplay")]
public class TextsToDisplay : ScriptableObject
{
    [SerializeField]
    public List<Phrase> phrases;
    [SerializeField]
    public float delayForWrite = 0.04f;
    [SerializeField]
    public Sprite spriteChar1;
    [SerializeField]
    public Sprite spriteChar2;
    [SerializeField]
    public bool LoadNextSceneInEnd = false;
}

[System.Serializable]
public class Phrase
{
    [SerializeField]
    public string nameOfSpeaker;
    [SerializeField] [TextArea(5, 5)]
    public string text;
}