using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
public class PracticeWithTrainingDummy : QuestStep
{
    private Quest quest;
    private GameObject trainingDummy;
    private GameObject visualIndicatorObject;
    private SpriteRenderer visualIndicator;
    private bool isPlayerInRange;
    private string dialogueKnotName = "TrainingDummyDialogue";
    private PlayerControls inputActions;
    private GameObject captainNPC;
    private SpriteRenderer captainNPCMarker;
    private bool hasCompletedTraining;

    [Header("Quest Sprite")]
    [SerializeField] private Sprite questQuestionMark; //might not be needed 

    [Header("Skill Check")]
    [SerializeField] private SkillCheckSetup skillCheckSetup; // Reference to the SkillCheckSetup script

    private void Awake() 
    {
        isPlayerInRange = false;
        hasCompletedTraining = false;
        inputActions = new PlayerControls();
        inputActions.Enable();
    }

    private void Start() 
    {
        trainingDummy = GameObject.FindWithTag("TrainingDummy");
        if(trainingDummy == null)
        {
            Debug.Log("Training Dummy not found");
            return;
        }
        visualIndicatorObject = trainingDummy.transform.GetChild(0).gameObject;   
        
        captainNPC = GameObject.FindWithTag("Captain");
        captainNPCMarker = captainNPC.transform.GetChild(0).GetComponent<SpriteRenderer>();
        quest = QuestManager.instance.GetQuestById(base.questID);
        
        GameEventsManager.instance.dialogueEvents.onDialogueStart += OnDialogueStart;
        GameEventsManager.instance.dialogueEvents.onDialogueComplete += OnDialogueComplete;

        skillCheckSetup = GameObject.FindWithTag("EventsManager").GetComponent<SkillCheckSetup>();
        // Setup the skill check UI instance based on the quest step index
        if (skillCheckSetup != null)
        {
            skillCheckSetup.SetupSkillCheck();
        }
        else
        {
            Debug.LogError("SkillCheckSetup is not assigned.");
        }
    }
    
    private void Update()
    {
        if(isPlayerInRange)
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
    }
    
    private void OnDialogueStart()
    {
        if(!isPlayerInRange)
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
        hasCompletedTraining = true;
        captainNPCMarker.color = Color.yellow;
        visualIndicatorObject.SetActive(false);
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
        string state = hasCompletedTraining ? "true" : "false";
        string status = state;
        ChangeState(state, status);
    }
    
    protected override void SetQuestStepState(string state)
    {
        if(state == "true")
        {
            hasCompletedTraining = true;
        }else {
            hasCompletedTraining = false;
        }
    }
}
