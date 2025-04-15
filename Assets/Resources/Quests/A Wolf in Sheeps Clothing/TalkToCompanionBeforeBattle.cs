using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
public class TalkToCompanionBeforeBattle : QuestStep
{
    private Quest quest;
    private bool hasTalkedToCompanion;
    private bool isPlayerInRange;
    private GameObject visualIndicatorObject;
    private SpriteRenderer visualIndicator;
    private GameObject companionNPC;
    private string dialogueKnotName = "talk_to_companion";
    
    private PlayerControls inputActions;
    [Header("Sprites")]
    [SerializeField] private Sprite questQuestionMark;
    
    private void Awake() {
        hasTalkedToCompanion = false;
        isPlayerInRange = false;
        inputActions = new PlayerControls();
        inputActions.Enable();
    }
    
    private void Start() {
        companionNPC = GameObject.FindWithTag("Healer");
        if(companionNPC == null) {
            Debug.Log("Companion NPC not found");
            return;
        }
        
        visualIndicatorObject = companionNPC.transform.GetChild(0).gameObject;
        visualIndicator = visualIndicatorObject.GetComponent<SpriteRenderer>();
        quest = QuestManager.instance.GetQuestById(base.questID);
        
        GameEventsManager.instance.dialogueEvents.onDialogueStart += OnDialogueStart;
        GameEventsManager.instance.dialogueEvents.onDialogueComplete += OnDialogueComplete;
    }
    
    private void Update() {
        if(isPlayerInRange && !hasTalkedToCompanion)
        {
        
            if(inputActions.PlayerMovement.NPCInteraction.WasPressedThisFrame())
            {
                DialogueManager.GetInstance().EnterDialogue(dialogueKnotName);
            }
        }
        
        if(quest.currentQuestStepIndex == base.currentStepIndex && !hasTalkedToCompanion)
        {
            if(visualIndicator != null)
            {
                visualIndicator.sprite = questQuestionMark;
            }
        }
    }
    
    private void OnDestroy()
    {
        GameEventsManager.instance.dialogueEvents.onDialogueStart -= OnDialogueStart;
        GameEventsManager.instance.dialogueEvents.onDialogueComplete -= OnDialogueComplete;
        inputActions.Disable();
    }
    
    private void OnDialogueStart()
    {
        if(hasTalkedToCompanion)
        {
            return;
        }
        
        if(DialogueManager.GetInstance().currentKnotName == dialogueKnotName)
        {
            hasTalkedToCompanion = true;
            visualIndicator.enabled = false;
        }
    }
    
    private void OnDialogueComplete()
    {
        if(hasTalkedToCompanion)
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
        string state = hasTalkedToCompanion ? "true" : "false";
        string status = state;
        ChangeState(state, status);
    }
    
    protected override void SetQuestStepState(string state)
    {
        if(state == "true")
        {
            hasTalkedToCompanion = true;
        }else {
            hasTalkedToCompanion = false;
        }
        
        UpdateState();
    }
}
