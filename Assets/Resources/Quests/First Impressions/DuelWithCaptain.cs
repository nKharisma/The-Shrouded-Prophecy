using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
public class DuelWithCaptain : QuestStep
{
    private Quest quest;
    private GameObject captainNPC;
    private BoxCollider finishCollider;
    private GameObject visualIndicatorObject;
    private GameObject finishQuestIndicator;
    private SpriteRenderer visualIndicator;
    private bool isPlayerInRange;
    private string dialogueKnotName = "DuelWithCaptain";
    private PlayerControls inputActions;
    private bool hasCompletedDuel;
    
    //[Header("Sprites")]
    
    [Header("Skill Check")]
    [SerializeField] private SkillCheckSetup skillCheckSetup;
    
    private void Awake() {
        isPlayerInRange = false;
        hasCompletedDuel = false;
        inputActions = new PlayerControls();
        inputActions.Enable();
    }
    
    private void Start() {
        captainNPC = GameObject.FindWithTag("Captain");
        if(captainNPC == null)
        {
            Debug.Log("Captain NPC not found");
            return;
        }
        finishCollider = captainNPC.GetComponent<BoxCollider>();
        visualIndicatorObject = captainNPC.transform.GetChild(0).gameObject;
        visualIndicator = visualIndicatorObject.GetComponent<SpriteRenderer>();
        finishQuestIndicator = captainNPC.transform.GetChild(2).gameObject;
        quest = QuestManager.instance.GetQuestById(base.questID);
        
        GameEventsManager.instance.dialogueEvents.onDialogueStart += OnDialogueStart;
        GameEventsManager.instance.dialogueEvents.onDialogueComplete += OnDialogueComplete;
        
        skillCheckSetup = GameObject.FindWithTag("EventsManager").GetComponent<SkillCheckSetup>();
        if (skillCheckSetup != null)
        {
            skillCheckSetup.SetupSkillCheck();
        }
        else
        {
            Debug.LogError("SkillCheckSetup is not found in the scene.");
        }
    }
    
    private void Update() {
        if(isPlayerInRange)
        {
            if(inputActions.PlayerMovement.NPCInteraction.WasPressedThisFrame())
            {
                DialogueManager.GetInstance().EnterDialogue(dialogueKnotName);
            }
        }
    }
    
    private void OnDestroy() {
        GameEventsManager.instance.dialogueEvents.onDialogueStart -= OnDialogueStart;
        GameEventsManager.instance.dialogueEvents.onDialogueComplete -= OnDialogueComplete;
        inputActions.Disable();
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
        hasCompletedDuel = true;
        finishQuestIndicator.SetActive(true);
        finishCollider.enabled = true;
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
        string state = hasCompletedDuel ? "true" : "false";
        string status = state;
        ChangeState(state, status);
    }
    
    protected override void SetQuestStepState(string questStepState)
    {
        if(questStepState == "true")
        {
            hasCompletedDuel = true;
        }else {
            hasCompletedDuel = false;
        }
        
        UpdateState();
    }
}
