using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
public class TalkToCaptainQuestStep : QuestStep
{
    private Quest quest;
    private bool questStarted;
    private bool hasTalkedToCaptain;
    private bool isPlayerInRange;
    private GameObject captainNPC;
    private GameObject visualIndicatorObject;
    private SpriteRenderer visualIndicator;
    private string dialogueKnotName = "CaptainDialogue";
    private PlayerControls inputActions;
    
    [Header("Sprites")]
    [SerializeField] private Sprite questQuestionMark;
    //[SerializeField] private Sprite questExclamationMark;
    
    private void Awake() {
        hasTalkedToCaptain = false;
        isPlayerInRange = false;
        questStarted = false;
        inputActions = new PlayerControls();
        inputActions.Enable();
    }

    private void Start()
    {   
        captainNPC = GameObject.FindWithTag("Captain");
        if(captainNPC == null)
        {
            Debug.Log("Captain NPC not found");
            return;
        }
        
        visualIndicatorObject = captainNPC.transform.GetChild(0).gameObject;
        visualIndicator = visualIndicatorObject.GetComponent<SpriteRenderer>();
        quest = QuestManager.instance.GetQuestById(base.questID);
    
        GameEventsManager.instance.dialogueEvents.onDialogueStart += OnDialogueStart;
        GameEventsManager.instance.dialogueEvents.onDialogueComplete += OnDialogueComplete;
    }
    
    private void Update()
    {
        if(isPlayerInRange && !hasTalkedToCaptain)
        {
            if(inputActions.PlayerMovement.NPCInteraction.WasPressedThisFrame())
            {
                DialogueManager.GetInstance().EnterDialogue(dialogueKnotName);
            }
        }
        
        //Debug.Log(quest.currentStepIndex);
        //Debug.Log(base.currentStepIndex);
        
        //Debug.Log(quest.currentStepIndex == base.currentStepIndex);
        if (quest.questState == QuestState.In_Progress && quest.currentQuestStepIndex == base.currentStepIndex && !questStarted)
        {
            questStarted = true;
            visualIndicator.enabled = true;
            //Debug.Log("Enabling visual indicator");
        }
    }

    private void OnDestroy()
    {
        GameEventsManager.instance.dialogueEvents.onDialogueStart -= OnDialogueStart;
        GameEventsManager.instance.dialogueEvents.onDialogueComplete -= OnDialogueComplete;
    }
    
    private void OnDialogueStart()
    {
        if(hasTalkedToCaptain)
        {
            return;
        }
        
        if(DialogueManager.GetInstance().currentKnotName == dialogueKnotName)
        {
            hasTalkedToCaptain = true;
            visualIndicator.enabled = false;
            UpdateState(); //might have to change this later?
        }
    }
    
    private void OnDialogueComplete()
    {
        if(hasTalkedToCaptain)
        {
            visualIndicator.enabled = true;
            visualIndicator.sprite = questQuestionMark;
            visualIndicator.color = Color.gray;
            CompleteStep();
        }
    }
    private void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.CompareTag("Player"))
        {
            isPlayerInRange = true;
        }
    }
    
    private void OnTriggerExit(Collider collider)
    {
        if (collider.gameObject.CompareTag("Player"))
        {
            isPlayerInRange = false;
        }
    }
    
    private void UpdateState()
    {
        string state = hasTalkedToCaptain ? "true" : "false";
        string status = state;
        ChangeState(state, status);
    }
    
    protected override void SetQuestStepState(string state)
    {
        if(state == "true")
        {
            hasTalkedToCaptain = true;
        }else {
            hasTalkedToCaptain = false;
        }
        
        UpdateState();
    }
}
