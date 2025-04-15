using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
public class RecruitMira : QuestStep
{
    private Quest quest;
    private GameObject miraNPC;
    private GameObject visualIndicatorObject;
    private SpriteRenderer visualIndicator;
    private bool isPlayerInRange;
    private string dialogueKnotName = "recruit_mira";
    private PlayerControls inputActions;
    private bool hasCompletedRecruitMira;

    [Header("Skill Check")]
    [SerializeField] private SkillCheckSetup skillCheckSetup; // Reference to the SkillCheckSetup script

    private void Awake() 
    {
        isPlayerInRange = false;
        hasCompletedRecruitMira = false;
        inputActions = new PlayerControls();
        inputActions.Enable();
    }

    private void Start() 
    {
        miraNPC = GameObject.FindWithTag("Mira");
        if(miraNPC == null)
        {
            Debug.Log("Mira NPC not found");
            return;
        }
        visualIndicatorObject = miraNPC.transform.GetChild(0).gameObject;
        visualIndicator = visualIndicatorObject.GetComponent<SpriteRenderer>();
        quest = QuestManager.instance.GetQuestById(base.questID);
        
        GameEventsManager.instance.dialogueEvents.onDialogueStart += OnDialogueStart;
        GameEventsManager.instance.dialogueEvents.onDialogueComplete += OnDialogueComplete;
        
        string status = "Talk to Mira";
        ChangeState("", status);
    }
    
    private void Update()
    {
        if(isPlayerInRange && !hasCompletedRecruitMira)
        {
            if(inputActions.PlayerMovement.NPCInteraction.WasPressedThisFrame())
            {
                DialogueManager.GetInstance().EnterDialogue(dialogueKnotName);
            }
        }
        
        if(quest.currentQuestStepIndex == base.currentStepIndex)
        {
            visualIndicatorObject.SetActive(true);
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
        if(hasCompletedRecruitMira)
        {
            return;
        }
        
        if(DialogueManager.GetInstance().currentKnotName == dialogueKnotName)
        {
            visualIndicatorObject.SetActive(false);
        }
    }
    
    private void OnDialogueComplete()
    {
        hasCompletedRecruitMira = true;
        CompanionManager.instance.RecruitCompanion("Mira");
        UpdateState();
        CompleteStep();
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
        string state = hasCompletedRecruitMira ? "true" : "false";
        string status = "";
        if(state == "true")
        {
            status = "You have recruited Mira.";
        }else {
            status = "Talk to Mira";
        }
        ChangeState(state, status);
    }

	protected override void SetQuestStepState(string questStepState)
	{
		hasCompletedRecruitMira = questStepState == "true";
		UpdateState();
	}
}
