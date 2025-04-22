using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
public class LeaveTheCity : QuestStep
{
    private Quest quest;
    private bool questStarted;
    private bool hasLeftCity;
    private GameObject captainNPC;
    private bool isPlayerInRange;
    private GameObject visualIndicatorObject;
    private SpriteRenderer visualIndicator;
    
    private string dialogueKnotName = "LeaveNowOrLater";
    private PlayerControls inputActions;
    
    [Header("Sprites")]
    [SerializeField] private Sprite questQuestionMark;
    //[SerializeField] private Sprite questExclamationMark;
    
    private void Awake() {
        hasLeftCity = false;
        isPlayerInRange = false;
        questStarted = false;
        inputActions = new PlayerControls();
        inputActions.Enable();
    }

    private void Start()
    {   
        //see about optimzing this later since the cpatain is used in mutliple scripts
        captainNPC = GameObject.FindWithTag("Captain");
        if(captainNPC == null)
        {
            Debug.Log("Captain NPC not found");
            return;
        }
    
        visualIndicatorObject = captainNPC.transform.GetChild(0).gameObject;
        visualIndicator = visualIndicatorObject.GetComponent<SpriteRenderer>();
        quest = QuestManager.instance.GetQuestById(base.questID);
        
        GameEventsManager.instance.dialogueEvents.onDialogueStart += OnDialogueStart;
        GameEventsManager.instance.dialogueEvents.onDialogueComplete += OnDialogueComplete;
    }

    private void Update()
    {
        if(isPlayerInRange && !hasLeftCity)
        {
            if(inputActions.PlayerMovement.NPCInteraction.WasPressedThisFrame())
            {
                DialogueManager.GetInstance().EnterDialogue(dialogueKnotName);
                //Debug.Log("Player has left the city");
            }
        }
        
        if (quest.questState == QuestState.In_Progress && quest.currentQuestStepIndex == base.currentStepIndex && !questStarted)
        {
            questStarted = true;
            visualIndicator.enabled = true;
            //Debug.Log("Enabling visual indicator");
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
        if(hasLeftCity)
        {
            return;
        }
        
        if(DialogueManager.GetInstance().currentKnotName == dialogueKnotName)
        {
            visualIndicator.enabled = false;
        }
    }
    
    private void OnDialogueComplete()
    {
        if(hasLeftCity)
        {
            CompleteStep();
            return;
        }
        
        // Check the player's choice
        string playerChoice = DialogueManager.GetInstance().GetLastSelectedChoice();
        Debug.Log("Player choice: " + playerChoice);
        
        if (playerChoice == "I'm ready to go.")
        {
            hasLeftCity = true;
            UpdateState();
            CompleteStep();
            Vector3 spawnPosition = new Vector3(-44.5f, 14.72f, 48.59f);
            StartCoroutine(WorldSaveGameManager.instance.LoadSceneInGame(2, spawnPosition));        
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
        string state = hasLeftCity ? "true" : "false";
        string status = "";
        if(state == "true")
        {
            status = "You have left the city.";
        }else {
            status = "You are still in the city.";
        }
        ChangeState("", status);
    }
    
    protected override void SetQuestStepState(string state)
    {
        
    }
}