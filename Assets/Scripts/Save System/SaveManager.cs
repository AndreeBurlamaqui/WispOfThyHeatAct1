using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BayatGames.SaveGameFree;
using System;

public class SaveManager : MonoBehaviour
{
public static SaveManager Instance { get; private set; }

    public SaveSlotSO CurrentSlot { get; private set; }

#if UNITY_EDITOR
    [SerializeField] private List<SavePoints> debugSaveList;
#endif
    private void Awake()
    {
        Instance = this;
        SaveGame.Serializer = new BayatGames.SaveGameFree.Serializers.SaveGameJsonSerializer();

        Debug.Log("Remember to set encoder true", this);
        SaveGame.Encode = false;
    }

    public void SlotChosen(SaveSlotSO saveSlot)
    {

        if (LoadSaveSlot(saveSlot.slotID) != null)
        {
            CurrentSlot = LoadSaveSlot(saveSlot.slotID);
            SceneLoader.Instance.ContinueGame();
        }
        else
        {
            CurrentSlot = Instantiate(saveSlot);
            SceneLoader.Instance.NewGame();
        }

#if UNITY_EDITOR
        debugSaveList = new List<SavePoints>(CurrentSlot.saveList);
#endif
    }

    public void SaveCurrentSlot()
    {
        SaveGame.Save<SaveSlotSO>("saveslot" + CurrentSlot.slotID.ToString(), CurrentSlot);
    }

    #region Save Variables Overloads
    public void SaveVariable(SaveName name, bool variable)
    {
        if (CurrentSlot.saveList.Exists(x => x.saveName == name))
        {
            int indexSaveList = CurrentSlot.saveList.FindIndex(x => x.saveName == name);

            CurrentSlot.saveList[indexSaveList].value = variable;
            Debug.Log("Variable: " + name + " already saved. Adding " + variable);

        }
        else
        {
            CurrentSlot.saveList.Add(new SavePoints(name, variable));
            Debug.Log("Create variable: " + name + " with value of: " + variable);

        }
    }
    public void SaveVariable(SaveName name, float variable)
    {
        if (CurrentSlot.saveList.Exists(x => x.saveName == name))
        {
            int indexSaveList = CurrentSlot.saveList.FindIndex(x => x.saveName == name);

            CurrentSlot.saveList[indexSaveList].value = variable;
            Debug.Log("Variable: " + name + " already saved. Adding " + variable);
        }
        else
        {
            CurrentSlot.saveList.Add(new SavePoints(name, variable));
            Debug.Log("Create variable: " + name + " with value of: " + variable);
        }
    }
    public void SaveVariable(SaveName name, string variable)
    {
        if (CurrentSlot.saveList.Exists(x => x.saveName == name))
        {
            int indexSaveList = CurrentSlot.saveList.FindIndex(x => x.saveName == name);

            CurrentSlot.saveList[indexSaveList].value = variable;
            Debug.Log("Variable: " + name + " already saved. Adding " + variable);

        }
        else
        {
            CurrentSlot.saveList.Add(new SavePoints(name, variable));
            Debug.Log("Create variable: " + name + " with value of: " + variable);

        }
    }
    public void SaveVariable(SaveName name, Vector3 variable)
    {
        CheckVariable(name, out Vector3 checkVector);

        if (CurrentSlot.saveList.Exists(x => x.saveName == name))
        {
            int indexSaveList = CurrentSlot.saveList.FindIndex(x => x.saveName == name);

            CurrentSlot.saveList[indexSaveList].value = variable;
            Debug.Log("Variable: " + name + " already saved. Adding " + variable);

        }
        else
        { 
            CurrentSlot.saveList.Add(new SavePoints(name, variable));
            Debug.Log("Create variable: " + name + " with value of: " + variable);

        }
    }

    #endregion

    #region Check Variables Overloads

    /// <summary>
    /// Returns "false" if variable not found
    /// </summary>
    /// <param name="name">Name of the variable to check</param>
    public bool CheckVariable(SaveName name)
    {

        bool variable = false;

        if(CurrentSlot.saveList.Exists(x => x.saveName == name))
        {

            variable = true;
        }

        return variable;

    }

    /// <summary>
    /// Returns "float.NaN" if variable not found
    /// </summary>
    /// <param name="name">Name of the variable to check</param>
    /// <param name="variable">Variable to get. Use this outside if and then check the variable in a if</param>
    public void CheckVariable(SaveName name, out float variable)
    {
        variable = float.NaN;

        if(CurrentSlot.saveList.Exists(x => x.saveName == name))
        {

            int indexSaveList = CurrentSlot.saveList.FindIndex(x => x.saveName == name);

            variable = (float)CurrentSlot.saveList[indexSaveList].value;

        }

    }

    /// <summary>
    /// Returns "null" if variable not found
    /// </summary>
    /// <param name="name">Name of the variable to check</param>
    /// <param name="variable">Variable to get. Use this outside if and then check the variable in a if</param>
    public void CheckVariable(SaveName name, out string variable)
    {
        variable = null;

        if (CurrentSlot.saveList.Exists(x => x.saveName == name))
        {
            int indexSaveList = CurrentSlot.saveList.FindIndex(x => x.saveName == name);

            variable = CurrentSlot.saveList[indexSaveList].value.ToString();
        }

    }

    /// <summary>
    /// Returns "Vector3.negativeInfinity" if variable not found
    /// </summary>
    /// <param name="name">Name of the variable to check</param>
    /// <param name="variable">Variable to get. Use this outside if and then check the variable in a if</param>
    public void CheckVariable(SaveName name, out Vector3 variable)
    {
        variable = Vector3.negativeInfinity;

        if (CurrentSlot.saveList.Exists(x => x.saveName == name))
        {
            int indexSaveList = CurrentSlot.saveList.FindIndex(x => x.saveName == name);

            variable = (Vector3)CurrentSlot.saveList[indexSaveList].value;
            
        }


    }
    #endregion

    public SaveSlotSO LoadSaveSlot(int slotID)
    {
        if (CheckSaveSlot(slotID))
        {
            SaveSlotSO saveSlot = SaveGame.Load<SaveSlotSO>("saveslot" + slotID.ToString());

            return saveSlot;
        }

        return null;
    }

    public bool CheckSaveSlot(int slotID)
    {
        return SaveGame.Exists("saveslot" + slotID);
    }

    public void DeleteSaveSLot(int slotID)
    {
        SaveGame.Delete("saveslot" + slotID);
    }

}
