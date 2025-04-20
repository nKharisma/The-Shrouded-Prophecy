using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
public class DefeatTheSlimes : QuestStep
{
    private Quest quest;
    private bool hasCompletedBattleWithSlimes;
    private bool isPlayerInRange;
    private PlayerControls inputActions;
    private SkillCheckSetup skillCheckSetup;
    
    private GameObject companionNPC;
    private GameObject visualIndicatorObject;
    private SpriteRenderer visualIndicator;
    private string dialogueKnotName = "slime_battle";
    [Header("Slime Colliders")]
    private GameObject[] slimes;
    
    
    private void Awake() {
        hasCompletedBattleWithSlimes = false;
        isPlayerInRange = false;
        inputActions = new PlayerControls();
        inputActions.Enable();
        
        slimes = GameObject.FindGameObjectsWithTag("Slime");
        if (slimes.Length == 0)
        {
            Debug.Log("No slimes found in the scene.");
        }
    }
    
    private void Start() {
        quest = QuestManager.instance.GetQuestById(base.questID);
        
        companionNPC = GameObject.FindWithTag("Healer");
        if (companionNPC == null)
        {
            Debug.LogError("Companion NPC not found.");
            return;
        }

        visualIndicatorObject = companionNPC.transform.GetChild(0).gameObject;
        visualIndicator = visualIndicatorObject.GetComponent<SpriteRenderer>();
        
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
        if (isPlayerInRange && !hasCompletedBattleWithSlimes)
        {
            if (inputActions.PlayerMovement.NPCInteraction.WasPressedThisFrame())
            {
                DialogueManager.GetInstance().EnterDialogue(dialogueKnotName);
                visualIndicator.enabled = false;
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
        if(hasCompletedBattleWithSlimes)
        {
            return;
        }
    }
    
    private void OnDialogueComplete()
    {
        hasCompletedBattleWithSlimes = true;
        foreach (GameObject slime in slimes)
        {
            Destroy(slime);
        }
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
        string state = hasCompletedBattleWithSlimes ? "true" : "false";
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
