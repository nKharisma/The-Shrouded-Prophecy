using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefeatTheSlimes : QuestStep
{
    private Quest quest;
    private bool hasCompletedBattleWithSlimes;
    private bool isPlayerInRange;
    private PlayerControls inputActions;
    private SkillCheckSetup skillCheckSetup;
    private string dialogueKnotName = "slime_battle";
    [Header("Slime Colliders")]
    private Collider[] slimeColliders;
    
    
    private void Awake() {
        hasCompletedBattleWithSlimes = false;
        isPlayerInRange = false;
        
        GameObject[] slimes = GameObject.FindGameObjectsWithTag("Slime");
        slimeColliders = new Collider[slimes.Length];
        for (int i = 0; i < slimes.Length; i++)
        {
            slimeColliders[i] = slimes[i].GetComponent<Collider>();
            if(slimeColliders[i] == null)
            {
                Debug.Log("Slime collider not found");
            }
        }
    }
    
    private void Start() {
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
    
    private void OnDestroy()
    {
        GameEventsManager.instance.dialogueEvents.onDialogueStart -= OnDialogueStart;
        GameEventsManager.instance.dialogueEvents.onDialogueComplete -= OnDialogueComplete;
        inputActions.Disable();
    }
    
    private bool IsSlimeCollider(Collider collider)
    {
        // Check if the collider belongs to one of the slimes
        foreach (Collider slimeCollider in slimeColliders)
        {
            if (collider == slimeCollider)
            {
                return true;
            }
        }
        return false;
    }
    
    private void Update()
    {
        if(isPlayerInRange && !DialogueManager.GetInstance().dialogueIsPlaying)
        {
            DialogueManager.GetInstance().EnterDialogue(dialogueKnotName);
        }
    }
    
    private void OnDialogueStart()
    {
        if(!isPlayerInRange || hasCompletedBattleWithSlimes)
        {
            return;
        }
    }
    
    private void OnDialogueComplete()
    {
        hasCompletedBattleWithSlimes = true;
        UpdateState();
        CompleteStep();
    }
    private void OnTriggerEnter()
    {
        if(!isPlayerInRange && IsSlimeCollider(GetComponent<Collider>()))
        {
            isPlayerInRange = true;
        }
    }
    
    private void OnTriggerExit()
    {
        if(isPlayerInRange && !IsSlimeCollider(GetComponent<Collider>()))
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
        ChangeState(state, status);
    }
    
    protected override void SetQuestStepState(string state)
    {
        if(state == "true")
        {
            hasCompletedBattleWithSlimes = true;
        }else {
            hasCompletedBattleWithSlimes = false;
        }
        
        UpdateState();
    }
}
