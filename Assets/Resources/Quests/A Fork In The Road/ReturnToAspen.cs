using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
public class ReturnToAspen : QuestStep
{
    private Quest quest;
    private GameObject aspenNPC;
    private GameObject visualIndicatorObject;
    private SpriteRenderer visualIndicator;
    private bool isPlayerInRange;
    private string dialogueKnotName = "return_to_aspen";
    private PlayerControls inputActions;
    private bool hasCompletedRecruitAspen;

    private void Awake() 
    {
        isPlayerInRange = false;
        hasCompletedRecruitAspen = false;
        inputActions = new PlayerControls();
        inputActions.Enable();
    }

    private void Start() 
    {
        aspenNPC = GameObject.FindWithTag("Immunity");
        if(aspenNPC == null)
        {
            Debug.Log("Aspen NPC not found");
            return;
        }
        visualIndicatorObject = aspenNPC.transform.GetChild(0).gameObject;
        visualIndicator = visualIndicatorObject.GetComponent<SpriteRenderer>();
        quest = QuestManager.instance.GetQuestById(base.questID);
        
        GameEventsManager.instance.dialogueEvents.onDialogueStart += OnDialogueStart;
        GameEventsManager.instance.dialogueEvents.onDialogueComplete += OnDialogueComplete;
        
        string status = "Return to Aspen";
        ChangeState("", status);
    }
    
    private void Update()
    {
        if(isPlayerInRange && !hasCompletedRecruitAspen)
        {
            if(inputActions.PlayerMovement.NPCInteraction.WasPressedThisFrame())
            {
                DialogueManager.GetInstance().EnterDialogue(dialogueKnotName);
            }
        }
        
        if(quest.currentQuestStepIndex == base.currentStepIndex)
        {
            //Debug.Log("Current step index: " + base.currentStepIndex);
            //Debug.Log("Quest step index: " + quest.currentQuestStepIndex);
            visualIndicator.enabled = true;
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
        if(hasCompletedRecruitAspen)
        {
            return;
        }
        
        if(DialogueManager.GetInstance().currentKnotName == dialogueKnotName)
        {
            visualIndicator.enabled = false;
            hasCompletedRecruitAspen = true;
        }
    }
    
    private void OnDialogueComplete()
    {
        if(hasCompletedRecruitAspen)
        {
            CompanionManager.instance.RecruitCompanion("Aspen");
            UpdateState();
            CompleteStep();
        }
    }
    
    private void OnTriggerEnter(Collider other) 
    {
        if(other.CompareTag("Player"))
        {
            isPlayerInRange = true;
        }
    }
    
    private void OnTriggerExit(Collider other) 
    {
        if(other.CompareTag("Player"))
        {
            isPlayerInRange = false;
        }
    }
    
    private void UpdateState()
    {
        string state = hasCompletedRecruitAspen ? "true" : "false";
        string status = "";
        if(state == "true")
        {
            status = "You have recruited Aspen.";
        }else {
            status = "Talk to Aspen";
        }
        ChangeState(state, status);
    }

	protected override void SetQuestStepState(string questStepState)
	{
		
	}
}
