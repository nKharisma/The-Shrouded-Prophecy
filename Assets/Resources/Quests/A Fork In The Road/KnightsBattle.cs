using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
public class KnightsBattle : QuestStep
{
    private Quest quest;
    private bool hasCompletedBattleWithKnights;
    private bool isPlayerInRange;
    private PlayerControls inputActions;
    private SkillCheckSetup skillCheckSetup;
    
    private GameObject aspenNPC;
    private GameObject visualIndicatorObject;
    private SpriteRenderer visualIndicator;
    private string dialogueKnotName = "knights_battle";
    private GameObject knights;
    private GameObject arrowIndicator;
    private SpriteRenderer arrowSpriteRenderer;
    
    
    private void Awake() {
        hasCompletedBattleWithKnights = false;
        isPlayerInRange = false;
        inputActions = new PlayerControls();
        inputActions.Enable();
        
        knights = GameObject.FindWithTag("Knights");
    }
    
    private void Start() {
        quest = QuestManager.instance.GetQuestById(base.questID);
        
        aspenNPC = GameObject.FindWithTag("Immunity");
        if (aspenNPC == null)
        {
            Debug.LogError("Companion NPC not found.");
            return;
        }

        visualIndicatorObject = aspenNPC.transform.GetChild(0).gameObject;
        visualIndicator = visualIndicatorObject.GetComponent<SpriteRenderer>();
        
        arrowIndicator = this.gameObject.transform.GetChild(0).gameObject;
        arrowSpriteRenderer = arrowIndicator.GetComponent<SpriteRenderer>();
        
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
        if (isPlayerInRange && DialogueManager.GetInstance().dialogueIsPlaying == false)
        {
            DialogueManager.GetInstance().EnterDialogue(dialogueKnotName);
            arrowSpriteRenderer.enabled = false;
        }
        else
        {
            arrowSpriteRenderer.enabled = true;
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
        if(hasCompletedBattleWithKnights)
        {
            return;
        }
    }
    
    private void OnDialogueComplete()
    {
        hasCompletedBattleWithKnights = true;
        Destroy(knights);
        visualIndicator.enabled = true;
        UpdateState();
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
        string state = hasCompletedBattleWithKnights ? "true" : "false";
        string status = "";
        if(state == "true")
        {
            status = "You have defeated the slimes!";
        }else {
            status = "Defeat the slimes!";
        }
        ChangeState("", status);
    }
    
    protected override void SetQuestStepState(string state)
    {
    
    }
}
