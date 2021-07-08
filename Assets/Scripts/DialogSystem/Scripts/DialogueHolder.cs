
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;
using MyBox;

[RequireComponent(typeof(BoxCollider2D))]
public class DialogueHolder : MonoBehaviour
{
    public List<Dialogue> dialogues = new List<Dialogue>();

    [Header("Auto Caller")]
    public bool isAutoCaller;
    
    [ConditionalField(nameof(isAutoCaller))] public bool canMove = true;
    [ConditionalField(nameof(isAutoCaller))] public bool isAutoTalk = false;
    [ConditionalField(nameof(isAutoCaller))] public bool usePref = true;
    [ConditionalField(nameof(isAutoCaller))] [SearchableEnum] public SaveName pref;

    [Space(50)]

    [SerializeField] public UnityEvent OnBeginTalkingEvent;
    [SerializeField] public UnityEvent OnMiddleTalkEvent;
    [SerializeField] public UnityEvent OnFinishTalkingEvent;

    private List<Line> conversation = new List<Line>();
    private string currentLocal;

    


    //if not AutoCaller get interactTooltip animator and check canInteract true when TriggerEnter and check false when TriggerExit
    private Animator interactTooltip;
    private bool canInteract;
    private PlayerControl inputControl;

    private void Awake()
    {
        currentLocal = DialogueContainer.Instance.currentLanguage;

        if (transform.childCount >= 1)
        {
            interactTooltip = transform.GetChild(0).GetComponent<Animator>();
        }

        inputControl = new PlayerControl();

        inputControl.Control_Global.Interact.canceled += OnInteract;

        OnFinishTalkingEvent.AddListener(OnFinishTalk);

    }
    private void OnEnable()
    {
        inputControl.Enable();
    }
    private void OnDisable()
    {
        inputControl.Disable();
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (isAutoCaller)
            {
                if (usePref)
                {
                    if (!SaveManager.Instance.CheckVariable(pref))
                    {
                        //call method to insta type
                        CheckDialogue();

                        SaveManager.Instance.SaveVariable(pref, true);
                        SaveManager.Instance.SaveCurrentSlot();
                    }
                }
                else
                {
                    CheckDialogue();
                }
            }
            else
            {

                //wait for input
                interactTooltip.SetBool("insideBox", true);
                canInteract = true;

            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (!isAutoCaller)
        {
            if (collision.CompareTag("Player"))
            {
                //block input
                interactTooltip.SetBool("insideBox", false);
                canInteract = false;
            }
        }
    }

    private void OnInteract(InputAction.CallbackContext obj)
    {
        if (canInteract)
        {
            canInteract = false;
            interactTooltip.SetTrigger("Clicked");
            CheckDialogue();
        }
    }
    private void CheckDialogue()
    {
        if (dialogues.Count > 1)
        {
            int chatIndex = 0;
            for (int x = 0; x < dialogues.Count; x++)
            {

                if (SaveManager.Instance.CheckVariable(dialogues[x].Condition))
                {
                    chatIndex = x;
                }

            }

            CheckLanguage(dialogues[chatIndex]);
        }
        else
        {
            CheckLanguage(dialogues[0]);
        }
    }

    private void CheckLanguage(Dialogue conv)
    {
        Debug.Log("Refactor when ID-Tagged SO", this);

        if (currentLocal == "English")
        {
            for (int x = 0; x < conv.linesEN.Length; x++)
            {
                conversation.Add(conv.linesEN[x]);
            }
        }
        

        if(currentLocal == "Portuguese"){
            for (int x = 0; x < conv.linesBR.Length; x++)
            {
                conversation.Add(conv.linesBR[x]);
            }
        }

        if (conversation.Count >= 1)
        {
            DialogueContainer.Instance.OnTalkTrigger(conversation, isAutoTalk, isAutoCaller? canMove : false, this);
        }

    }

    private void OnFinishTalk()
    {
        if (!usePref)
            Destroy(this);
    }

}
