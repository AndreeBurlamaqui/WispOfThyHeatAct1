

#if UNITY_EDITOR

using System.Collections.Generic;
using System.Collections;
using UnityEditor;
using System.IO;
using UnityEngine;

public class CSVtoSO
{

    [MenuItem("Tools/Tiny Cacto/Generate Dialogue")]
    public static void LoadCSV() {

        string dialogueCSVPath = EditorUtility.OpenFilePanel("Load CSV File","", "csv");

        if (!string.IsNullOrEmpty(dialogueCSVPath) && dialogueCSVPath.EndsWith(".csv"))
        {
            string rawText = File.ReadAllText(dialogueCSVPath);

            List<Dictionary<string, object>> rawCSVData = CSVReader.Read(rawText);

            PerformDialogueGeneration(rawCSVData);
        }
        else
        {
            if(EditorUtility.DisplayDialog("Null or Empty path", "Please select a valid .csv file", "Try Again", "Cancel"))
            {
                LoadCSV();
            }
        }

    } 


    private static void PerformDialogueGeneration(List<Dictionary<string, object>> csvData)
    {
        Line[] newLinesEN;
        Line[] newLinesBR;

        newLinesEN = new Line[csvData.Count];
        newLinesBR = new Line[csvData.Count];

        for (int x = 0; x < csvData.Count; x++)
        {
            Dictionary<string, object> _potentialDialogue = csvData[x];

            //English
            newLinesEN[x].CharacterName = _potentialDialogue["Character"].ToString() +", " + _potentialDialogue["CharTitle_English"].ToString();
            newLinesEN[x].DialogText = _potentialDialogue["Line_English"].ToString();

            //Portuguese
            newLinesBR[x].CharacterName = _potentialDialogue["Character"].ToString() + ", " +_potentialDialogue["CharTitle_Portuguese"].ToString();
            newLinesBR[x].DialogText = _potentialDialogue["Line_Portuguese"].ToString();
        }

        CreateScriptableObject(newLinesEN, newLinesBR);

    }

    private static void CreateScriptableObject(Line[] linesEN, Line[] linesBR)
    {
        Dialogue newDialogue = ScriptableObject.CreateInstance<Dialogue>();

        newDialogue.linesEN = linesEN;
        newDialogue.linesBR = linesBR;

        string path = EditorUtility.SaveFilePanelInProject("Where to save Dialogue", "newDialogue", "asset", "Please enter a name to the Dialogue");
        
        Debug.Log(path);

        AssetDatabase.CreateAsset(newDialogue, $"{path}");
        AssetDatabase.SaveAssets();
        
    }
}
#endif