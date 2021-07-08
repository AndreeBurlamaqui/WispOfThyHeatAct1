using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Collections;
using UnityEngine.UI;
using RedBlueGames.Tools.TextTyper;
using MyBox;

public class DialogueContainer : DialogueManager<DialogueContainer>
{
    //This is the script that will open the dialogue, type the conversation and then close the dialogue

    public string currentLanguage;


    [Header("Dialogue HUD")]
    public TextMeshProUGUI characterNameUI;
    public TextTyper textComponent;
    public Button dialogBox;
    public Button nextButton;
    //if Autocaller set waitTime variable
    public int waitTimeToNextLine;

    private int linePos;
    [ReadOnly] [SerializeField] private Animator dialogAnim;
    [ReadOnly] [SerializeField] private bool canType = false, move = false, autoTalker = false;
    private List<Line> conversation = new List<Line>();
    [ReadOnly] [SerializeField] private DialogueHolder whomTalked;


    [Header("Tooltip HUD")]
    public GameObject tooltip;



    [Header("Elan Vital HUD")]
    public GameObject elanVital;



    private void Awake()
    {
        dialogAnim = GetComponent<Animator>();
    }

    void Start()
    {
        Instance.OnTalk += OnTalkTrigger;
        Instance.OnFinishTalking += OnFinishTalkTrigger;
        currentLanguage = Lean.Localization.LeanLocalization.GetFirstCurrentLanguage();
    }



    public void StartTalk()
    {
        //now thats the dialog is opened, start talking

        if (autoTalker)
        {
            dialogBox.interactable = false;
        }
        else
        {
            dialogBox.interactable = true;
        }

        if (linePos >= 0 && linePos < conversation.Count)
        {

            whomTalked.OnMiddleTalkEvent.Invoke();

            textComponent.TypeText(conversation[linePos].DialogText, -0.8f);
            characterNameUI.SetText(conversation[linePos].CharacterName);

            if (!autoTalker)
            {
                if (dialogAnim.GetBool("NextButton"))
                {
                    dialogAnim.SetBool("NextButton", false);
                }
            }
            else
            {
                dialogAnim.SetBool("NextButton", false);
            }


            linePos++;

        }
        else
        {
            Instance.OnFinishTalkTrigger();
        }
    }
    public void OnTalkTrigger(List<Line> storageConv, bool isAutoTalk, bool canMove, DialogueHolder whoTalking)
    {
        if (!canType)
        {
            if (storageConv != null)
            {
                for (int x = 0; x < storageConv.Count; x++)
                {
                    Debug.Log("Addin conversation on storage");
                    conversation.Add(storageConv[x]);
                }
            }

            whomTalked = whoTalking;
            move = canMove;
            autoTalker = isAutoTalk;

            PlayerManager.Instance.MovementState = canMove;
            nextButton.gameObject.SetActive(false);

            whomTalked.OnBeginTalkingEvent.Invoke();
            dialogAnim.SetBool("ShowChat", true);
            canType = true;
            characterNameUI.SetText(conversation[linePos].CharacterName);

        }
    }

    public void OnFinishTalkTrigger()
    {

        //unblock Mithra Shooting
        //unblock Nasreen Movement
        whomTalked.OnFinishTalkingEvent.Invoke();

        linePos = 0;
        canType = false;
        conversation.Clear();
        characterNameUI.SetText(" ");
        textComponent.TypeText(" ");
        PlayerManager.Instance.MovementState = true;

        //set trigger close
        dialogAnim.SetBool("NextButton", false);

        dialogAnim.SetBool("ShowChat", false);



    }

    IEnumerator AutoTalkerTimer()
    {

        yield return new WaitForSeconds(waitTimeToNextLine);

        Debug.Log("Continue auto talking");

        Instance.StartTalk();
    }

    public void NextLineByButton()
    {

        /*if (!isAutoCaller)
        {
            if (dialogAnim.GetBool("NextButton"))
            {
                dialogAnim.SetBool("NextButton", false);
                Debug.Log("Continue talking by button");

                Instance.StartTalk();
            }
        }*/
        Instance.StartTalk();

    }

    public void LineFinished()
    {
        if (!autoTalker)
        {
            if (!dialogAnim.GetBool("NextButton"))
            {
                nextButton.gameObject.SetActive(true);

                dialogAnim.SetBool("NextButton", true);
            }
        }
        else
        {
            StartCoroutine(AutoTalkerTimer());
        }
    }

}
