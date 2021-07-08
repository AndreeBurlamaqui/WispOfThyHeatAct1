using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.InputSystem;
using MyBox;

public class MainMenu :  MonoBehaviour
{
    [Header("Main Menu")]
    public GameObject MainView, MainCam;
    public TextMeshProUGUI playLabel;


    [Header("Options Menu")]
    public GameObject OptionsView, OptionsCam;
    public Button backAccessibility;
    public Toggle autoAim, infLife;
    public GameObject clearSavePOPUP;


    [Header("Slot Menu")]
    public GameObject slotView;
    public GameObject slotCam, playSlotView;
    private SlotHolder newSelectedSlot;

[Separator]

    public GameObject firstSelectOptions;
    public RectTransform setas;

    [Space] [SerializeField] private PlayerControl inputControl;

    private InputHandleScript gManager;

    void Start()
    {

        gManager = GameObject.FindWithTag("GameManager").GetComponent<InputHandleScript>();

        playLabel.transform.parent.GetComponent<Button>().Select();

        //clearSavePOPUP.SetActive(false);

        CheckPlayText();
    }

    #region input manager
    private void Awake()
    {
        inputControl = new PlayerControl();
        inputControl.Control_Global.Back.performed += OnBack;
    }

    private void OnEnable()
    {
        inputControl.Enable();   
    }

    private void OnDisable()
    {
        inputControl.Disable();
    }
    #endregion

    public void QuitButton()
    {
#if UNITY_EDITOR
        // Application.Quit() does not work in the editor so
        // UnityEditor.EditorApplication.isPlaying need to be set to false to end the game
        UnityEditor.EditorApplication.isPlaying = false;
#else
                         Application.Quit();
#endif
    }
    public void OnBack(InputAction.CallbackContext context) {

        Debug.Log("GOBACK");

        if (OptionsView.activeSelf)
        {
            if (clearSavePOPUP.activeSelf)
            {
                //fecha popup
                CloseClearSavePopup();
            }
            else
            {
                //volta main
                BackToMain();
            }
        }
    }

    #region Main Menu Methods
    public void PlayButton()
    {

        MainView.SetActive(false);
        MainCam.SetActive(false);
        slotView.SetActive(true);
        slotCam.SetActive(true);
    }

    public void CheckPlayText()
    {
        if (PlayerPrefs.GetInt("checkpointScene", 0) != 0)
        {

            playLabel.GetComponent<Lean.Localization.LeanLocalizedTextMeshProUGUI>().TranslationName = "Continuar";
            //setas.offsetMin = new Vector2(-1100, setas.offsetMin.y);
            //setas.offsetMax = new Vector2(1100, setas.offsetMax.y);

            setas.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, 1100);


        }
        else
        {
            playLabel.GetComponent<Lean.Localization.LeanLocalizedTextMeshProUGUI>().TranslationName = "Jogar";
            //setas.offsetMin = new Vector2(-100, setas.offsetMin.y);
            //setas.offsetMax = new Vector2(100, setas.offsetMax.y);

            setas.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, 100);

        }
    }
    #endregion

    #region Option Menu Methods

    public void OptionsButton()
    {
        MainView.SetActive(false);
        MainCam.SetActive(false);
        OptionsView.SetActive(true);
        OptionsCam.SetActive(true);

        firstSelectOptions.GetComponent<Button>().Select();

    }

    public void BackToMain()
    {
        MainView.SetActive(true);
        MainCam.SetActive(true);
        OptionsView.SetActive(false);
        OptionsCam.SetActive(false);

        playLabel.transform.parent.GetComponent<Button>().Select();

    }

    public void ChangeAutoAim()
    {
        Debug.Log("Refazer menu pro auto aim", this);
        bool state = !PlayerManager.Instance.IsAutoAimEnabled;
        autoAim.isOn = state;

        PlayerManager.Instance.IsAutoAimEnabled = state;

    }

    public void AutoAim(bool state, string device)
    {

        autoAim.isOn = state;
        PlayerManager.Instance.IsAutoAimEnabled = state;
        Debug.Log(PlayerManager.Instance.IsAutoAimEnabled);

    }

    public void InfinityLifeAssist()
    {

        if(PlayerPrefs.GetInt("InfinityLifes", 0) != 0) {
            //se for true
            PlayerPrefs.SetInt("InfinityLifes", 0);
            PlayerPrefs.Save();

            infLife.isOn = false;

        }
        else
        {
            PlayerPrefs.SetInt("InfinityLifes", 1);
            PlayerPrefs.Save();

            infLife.isOn = true;

        }


    }

    public void ShowClearSavePopup()
    {
        clearSavePOPUP.SetActive(true);
        GameObject.Find("Button - Ok").GetComponent<Button>().Select();
    }

    public void CloseClearSavePopup()
    {
        clearSavePOPUP.SetActive(false);
        firstSelectOptions.GetComponent<Button>().Select();
    }

    public void ClearSave()
    {

        PlayerPrefs.DeleteAll();
        CheckPlayText();
        CloseClearSavePopup();
    }

    #endregion

    #region Slot Menu Methods

    public void SlotGetter(SlotHolder selectedSlot)
    {

        //SaveManager.Instance.SlotChosen(selectedSlot._saveSlotI);

        Transform saveSlotsHolders = slotView.transform.GetChild(0);

        for (int x = 0; x < saveSlotsHolders.childCount; x++)
        {
            if(saveSlotsHolders.GetChild(x).GetComponent<SlotHolder>()._saveSlotI != selectedSlot._saveSlotI)
            {
                saveSlotsHolders.GetChild(x).gameObject.SetActive(false);
            }
        }

        newSelectedSlot = selectedSlot;
        playSlotView.SetActive(true);
    }

    public void StartGameBySlot()
    {
        SaveManager.Instance.SlotChosen(newSelectedSlot._saveSlotI);
    }

    public void DeleteSelectedSlot()
    {
        if (SaveManager.Instance.CheckSaveSlot(newSelectedSlot._saveSlotI.slotID))
        {
            SaveManager.Instance.DeleteSaveSLot(newSelectedSlot._saveSlotI.slotID);
            newSelectedSlot.IsNewGame();
        }
    }

    public void BackToSelection()
    {
        Transform saveSlotsHolders = slotView.transform.GetChild(0);

        for (int x = 0; x < saveSlotsHolders.childCount; x++)
        {
                   
            saveSlotsHolders.GetChild(x).gameObject.SetActive(true);
      
        }
        playSlotView.SetActive(false);
    }

    #endregion
}
