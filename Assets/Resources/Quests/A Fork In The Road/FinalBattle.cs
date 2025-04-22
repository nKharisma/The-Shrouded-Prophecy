using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
public class FinalBattle : QuestStep
{
    private Quest quest;
    private bool hasCompletedFinalBattle;
    private bool isPlayerInRange;
    private PlayerControls inputActions;
    private SkillCheckSetup skillCheckSetup;
    private string finalQuestID = "AForkInTheRoadSO";
    private GameObject aspenNPC;
    private GameObject visualIndicatorObject;
    private SpriteRenderer visualIndicator;
    private string dialogueKnotName = "confrontation";
    private string playerChoice = "";
    private bool skillCheckTriggered = false;
    private void Awake() {
        hasCompletedFinalBattle = false;
        isPlayerInRange = false;
        inputActions = new PlayerControls();
        inputActions.Enable();
    }
    
    private void Start() {
        quest = QuestManager.instance.GetQuestById(base.questID);
        

        visualIndicatorObject = this.gameObject.transform.GetChild(0).gameObject;
        visualIndicator = visualIndicatorObject.GetComponent<SpriteRenderer>();
        visualIndicator.enabled = false;
        
        GameEventsManager.instance.dialogueEvents.onDialogueStart += OnDialogueStart;
        GameEventsManager.instance.dialogueEvents.onDialogueComplete += OnDialogueComplete;
        
        skillCheckSetup = GameObject.FindWithTag("EventsManager").GetComponent<SkillCheckSetup>();
        // Setup the skill check UI instance based on the quest step index
    }
    
    private void Update()
    {
        if (isPlayerInRange && DialogueManager.GetInstance().dialogueIsPlaying == false)
        {
            DialogueManager.GetInstance().EnterDialogue(dialogueKnotName);
            visualIndicator.enabled = false;
        }
        
        if(isPlayerInRange && DialogueManager.GetInstance().dialogueIsPlaying == true && !skillCheckTriggered)
        {
            playerChoice = DialogueManager.GetInstance().GetLastSelectedChoice();
            
            if(playerChoice == "I was sent here to gather information.")
            {
                skillCheckSetup.SetupSkillCheckByIndex(0);
                skillCheckTriggered = true;
            }else if(playerChoice == "This was always temporary. Just doing my job.")
            {
                skillCheckSetup.SetupSkillCheckByIndex(1);
                skillCheckTriggered = true;
            }
        }
        else
        {
            visualIndicator.enabled = true;
        }
    }
    
    private void OnDestroy()
    {
        GameEventsManager.instance.dialogueEvents.onDialogueStart -= OnDialogueStart;
        GameEventsManager.instance.dialogueEvents.onDialogueComplete -= OnDialogueComplete;
        inputActions.Disable();
        GameEventsManager.instance.questEvents.CompleteQuest(finalQuestID);
    }

    private void OnDialogueStart()
    {
        if(hasCompletedFinalBattle)
        {
            return;
        }
    }
    
    private void OnDialogueComplete()
    {
        hasCompletedFinalBattle = true;
        //UpdateState();
        CompleteStep();
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
    }
    
    protected override void SetQuestStepState(string state)
    {
    
    }
}

