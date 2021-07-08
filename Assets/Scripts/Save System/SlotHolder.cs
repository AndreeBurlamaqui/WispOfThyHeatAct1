using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class SlotHolder : MonoBehaviour
{
    public MainMenu menuHandler;
    [SerializeField] private SaveSlotSO saveSlot;
    public SaveSlotSO _saveSlotI;

    public TextMeshProUGUI slotName, slotProgress, slotTimePlayed, slotDifficulty;

    void Start()
    {
        _saveSlotI = Instantiate(saveSlot);

        GetComponent<Button>().onClick.AddListener(() => menuHandler.SlotGetter(GetComponent<SlotHolder>()));

        IsNewGame();
    }

    public void IsNewGame()
    {
        if (!SaveManager.Instance.CheckSaveSlot(_saveSlotI.slotID))
        {
            //load newgame Preset
            slotName.GetComponent<Lean.Localization.LeanLocalizedTextMeshProUGUI>().TranslationName = "Slot_NovoJogo";
        }
        else
        {
            //load SO

            _saveSlotI = SaveManager.Instance.LoadSaveSlot(_saveSlotI.slotID);

            slotName.GetComponent<Lean.Localization.LeanLocalizedTextMeshProUGUI>().TranslationName = "Continuar";
            slotProgress.alpha = 1f;
            slotTimePlayed.alpha = 1f;
            slotDifficulty.alpha = 1f;

            for(int x = 0; x < _saveSlotI.saveList.Count; x++)
            {
                if(_saveSlotI.saveList[x].saveName == SaveName.SlotProgress)
                {
                    slotProgress.text = "- " + _saveSlotI.saveList[x].value + "%";
                }

                if (_saveSlotI.saveList[x].saveName == SaveName.TimePlayed)
                {
                    slotTimePlayed.text = "| " + _saveSlotI.saveList[x].value;
                }

                if (_saveSlotI.saveList[x].saveName == SaveName.Difficulty)
                {
                    slotDifficulty.GetComponent<Lean.Localization.LeanLocalizedTextMeshProUGUI>().TranslationName = _saveSlotI.saveList[x].value.ToString();
                }
            }
        }
    }
}
