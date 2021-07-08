using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.IO;


[System.Serializable]
public struct Line{
    public string CharacterName;

    [TextArea(2,5)]
    public string DialogText;

    public AudioClip voice;
}

//[CreateAssetMenu(fileName = "New Dialogue", menuName = "Dialogues")]
public class Dialogue : ScriptableObject
{
    public SaveName Condition;
    public Line[] linesEN;
    public Line[] linesBR;

}

