using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
public class LeaveTheTown : QuestStep
{
    private Quest quest;
    private bool questStarted;
    private bool hasLeftTown;
    private GameObject erynNPC;
    private bool isPlayerInRange;
    
    private string dialogueKnotName = "go_on";
    private PlayerControls inputActions;
    //[SerializeField] private Sprite questExclamationMark;
    
    private void Awake() {
        hasLeftTown = false;
        isPlayerInRange = false;
        questStarted = false;
        inputActions = new PlayerControls();
        inputActions.Enable();
    }

    private void Start()
    {   
        //see about optimzing this later since the cpatain is used in mutliple scripts
        erynNPC = GameObject.FindWithTag("Eryn");
        if(erynNPC == null)
        {
            Debug.Log("Eryn NPC not found");
            return;
        }
        quest = QuestManager.instance.GetQuestById(base.questID);
        
        GameEventsManager.instance.dialogueEvents.onDialogueStart += OnDialogueStart;
        GameEventsManager.instance.dialogueEvents.onDialogueComplete += OnDialogueComplete;
    }

    private void Update()
    {
        if(isPlayerInRange && !hasLeftTown)
        {
            if(inputActions.PlayerMovement.NPCInteraction.WasPressedThisFrame())
            {
                DialogueManager.GetInstance().EnterDialogue(dialogueKnotName);
                //Debug.Log("Player has left the city");
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
        if(hasLeftTown)
        {
            return;
        }
    }
    
    private void OnDialogueComplete()
    {
        if(hasLeftTown)
        {
            CompleteStep();
            return;
        }
        
        // Check the player's choice
        string playerChoice = DialogueManager.GetInstance().GetLastSelectedChoice();
        Debug.Log("Player choice: " + playerChoice);
        
        if (playerChoice == "I'll find him." || playerChoice == "Do you think he's still out there?")
        {
            hasLeftTown = true;
            UpdateState();
            CompleteStep();
            Vector3 spawnPosition = new Vector3(-99.04f, 3.72f, -72.99f);
            StartCoroutine(WorldSaveGameManager.instance.LoadSceneInGame(4, spawnPosition));        
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
        string state = hasLeftTown ? "true" : "false";
        string status = "";
        if(state == "true")
        {
            status = "You have left the town.";
        }else {
            status = "You are still in the town.";
        }
        ChangeState("", status);
    }
    
    protected override void SetQuestStepState(string state)
    {
        
    }
}
