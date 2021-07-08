using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MyBox;
using System;

#if UNITY_EDITOR
using UnityEditor;
using System.Text.RegularExpressions;
using System.Globalization;
#endif


#region SaveTypes
[Serializable]
public class SavePoints : PropertyAttribute
{
    [SearchableEnum] public SaveName saveName;
    [HideInInspector] public object value;
    [SerializeField] public string inspectorValue;
    public SavePoints(SaveName _savename, bool _value)
    {
        saveName = _savename;
        value = _value;
        inspectorValue = _value.ToString();
    }

    public SavePoints(SaveName _savename, float _value)
    {
        saveName = _savename;
        value = _value;
        inspectorValue = _value.ToString();

    }
    public SavePoints(SaveName _savename, string _value)
    {
        saveName = _savename;
        value = _value;
        inspectorValue = _value.ToString();

    }
    public SavePoints(SaveName _savename, Vector3 _value)
    {
        saveName = _savename;
        value = _value;
        inspectorValue = _value.ToString();
    }

    
}

#endregion

#region SaveNames
public enum SaveName
{
    //Slot related
    SlotProgress, TimePlayed, Difficulty,

    //Player related
    IsDead, LifePoints,

    //Mithra related
    MithraFall, GotMithra,

    //Checkpoint related
    CheckpointScene, CheckpointSpawnPosition,

    //Story related
    MetLaleh, KardelenPray, MetAdelmurgh, MetAntHill,
    ChatNasreenMithraForest, ChatNasreenMithraGoingMountain, ChatNasreenMithraGoingMountainTip, ChatNasreenMithraAtTopMountain,
    FirstMetFisherd, MetFisherdAtThermal, MetFisherdAtMines, MetFisherdAtDeadBodies, ElanVital15, ElanVital75, ElanVital100,

    //Boss battles
    MetNozhan, LostToNozhan,
    MetGazdum, LostToGazdum,
    MetAseermagh, LostToAseermagh,
    MetKursvani, LostToKursvani,
    MetAuramazda, LostToAuramazdaStatue, LostToAuramazdaTrueForm
}
#endregion



[CreateAssetMenu(fileName = "Save Slot", menuName = "TinyCacto/Save Slot")]
public class SaveSlotSO : ScriptableObject
{
    public int slotID;
    [Space]
    [NamedArray(typeof(SaveName))] public List<SavePoints> saveList = new List<SavePoints>();


#if UNITY_EDITOR

    [Space]
    [Separator]
    [Space]


    [SearchableEnum][SerializeField] private SaveName newSaveName;
    private enum TypeSave{Bool, Float, String, Vector3 }; [SerializeField] private TypeSave typeSave;

    [SerializeField] [ConditionalField(nameof(typeSave), false, TypeSave.Bool)] private bool newboolVar;
    [SerializeField] [ConditionalField(nameof(typeSave), false, TypeSave.Float)] private float newFloatVar;
    [SerializeField] [ConditionalField(nameof(typeSave), false, TypeSave.String)] private string newStringVar;
    [SerializeField] [ConditionalField(nameof(typeSave), false, TypeSave.Vector3)] private Vector3 newVectorVar;


    private void OnValidate()
    {
        for(int x = 0; x < saveList.Count; x++)
        {
            if (saveList[x].inspectorValue == "True" || saveList[x].inspectorValue == "False") // Se for bool
            {
                saveList[x].value = Convert.ToBoolean(saveList[x].inspectorValue);

            } else if (!Regex.IsMatch(saveList[x].inspectorValue, @"[a-zA-Z]") && !saveList[x].inspectorValue.Contains("(")) // Se for float
            {
                CultureInfo ci = (CultureInfo)CultureInfo.CurrentCulture.Clone();
                ci.NumberFormat.CurrencyDecimalSeparator = ".";
                saveList[x].value = float.Parse(saveList[x].inspectorValue, NumberStyles.Any, ci);
            }
            else if (Regex.IsMatch(saveList[x].inspectorValue, @"[a-zA-Z]")) // se for string
            {
                saveList[x].value = saveList[x].inspectorValue.ToString();
            }
            else // se for vector 3
            {
                if (saveList[x].inspectorValue.StartsWith("(") && saveList[x].inspectorValue.EndsWith(")"))
                {
                    saveList[x].inspectorValue = saveList[x].inspectorValue.Substring(1, saveList[x].inspectorValue.Length - 2);
                }

                // split the items
                string[] sArray;


                sArray = saveList[x].inspectorValue.Split(',');


                // store as a Vector3
                CultureInfo ci = (CultureInfo)CultureInfo.CurrentCulture.Clone();
                ci.NumberFormat.CurrencyDecimalSeparator = ".";
                saveList[x].value = new Vector3( float.Parse(sArray[0], NumberStyles.Any, ci), float.Parse(sArray[1], NumberStyles.Any, ci), float.Parse(sArray[2], NumberStyles.Any, ci)); 
            }

            saveList[x].inspectorValue = saveList[x].value.ToString();
            Debug.Log(saveList[x].value + " Type: " + saveList[x].value.GetType());
        }
        
    }


    [CustomPropertyDrawer(typeof(NamedArrayAttribute))]
    public class NamedArrayDrawer : PropertyDrawer
    {
        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            // Properly configure height for expanded contents.
            return EditorGUI.GetPropertyHeight(property, label, property.isExpanded);
        }
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            // Replace label with enum name if possible.
            try
            {
                var config = attribute as NamedArrayAttribute;
                SerializedProperty key = property.FindPropertyRelative("saveName");

                GUIContent enum_label = new GUIContent(key.enumDisplayNames[key.enumValueIndex]);

                label = new GUIContent(enum_label);
            }
            catch
            {
                // keep default label
            }
            EditorGUI.PropertyField(position, property, label, property.isExpanded);
        }
    }

    [ButtonMethod]
    private void SetVariable()
    {
                switch (typeSave)
            {
                case TypeSave.Bool:
                        saveList.Add(new SavePoints(newSaveName, newboolVar));
                    break;
                case TypeSave.Float:
                        saveList.Add(new SavePoints(newSaveName, newFloatVar));
                    break;
                case TypeSave.String:
                        saveList.Add(new SavePoints(newSaveName, newStringVar));
                    break;
                case TypeSave.Vector3:
                        saveList.Add(new SavePoints(newSaveName, newVectorVar));
                    break;


                default: Debug.Log("<color=red>No type save selected</color>");
                    break;
            }

        
            newSaveName = 0;
            newboolVar = false;
            newFloatVar = 0;
            newStringVar = "";
            newVectorVar = Vector3.zero;
 
    }

#endif
}

public class NamedArrayAttribute : PropertyAttribute
{
    public Type TargetEnum;
    public NamedArrayAttribute(Type TargetEnum)
    {
        this.TargetEnum = TargetEnum;
    }
}

