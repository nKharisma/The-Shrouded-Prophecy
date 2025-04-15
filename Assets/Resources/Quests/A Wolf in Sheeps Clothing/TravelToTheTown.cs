using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TravelToTheTown : QuestStep
{
    private Quest quest;
    private bool isPlayerInRange;
    private bool hasCompletedTravelToTown;
    private PlayerControls inputActions;
    private GameObject miraNPC;
    private BoxCollider miraCollider;
    private GameObject visualIndicatorObject;
    //private SpriteRenderer visualIndicator;
    private string dialogueKnotName = "travel_to_town";
    
    private void Awake() {
        isPlayerInRange = false;
        hasCompletedTravelToTown = false;
        inputActions = new PlayerControls();
        inputActions.Enable();
    }
    
    private void Start() {
        miraNPC = GameObject.FindWithTag("Healer");
        if(miraNPC == null)
        {
            Debug.Log("Mira NPC not found");
        }
        
        miraCollider = miraNPC.GetComponent<BoxCollider>();
        if(miraCollider == null)
        {
            Debug.Log("Mira NPC collider not found");
        }
        
        miraCollider.enabled = true;
        visualIndicatorObject = miraNPC.transform.GetChild(0).gameObject;
        //visualIndicator = visualIndicatorObject.GetComponent<SpriteRenderer>();
    
        quest = QuestManager.instance.GetQuestById(base.questID);
        
        GameEventsManager.instance.dialogueEvents.onDialogueStart += OnDialogueStart;
        GameEventsManager.instance.dialogueEvents.onDialogueComplete += OnDialogueComplete;
    }
    
    private void OnDestroy()
    {
        GameEventsManager.instance.dialogueEvents.onDialogueStart -= OnDialogueStart;
        GameEventsManager.instance.dialogueEvents.onDialogueComplete -= OnDialogueComplete;
        inputActions.Disable();
    }

	private void Update()
	{
		if(isPlayerInRange && !hasCompletedTravelToTown)
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
	
	private void OnDialogueStart()
	{
	    if(!hasCompletedTravelToTown)
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
        hasCompletedTravelToTown = true;
        miraCollider.enabled = false;
        UpdateState();
        //here to transfer the player to the town
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
        string state = hasCompletedTravelToTown ? "true" : "false";
        string status = "";
        if(state == "true")
        {
            status = "You have traveled to the town.";
        }
        else
        {
            status = "Talk to Mira about traveling to the town.";
        }
        ChangeState(state, status);
	}

	protected override void SetQuestStepState(string questStepState)
	{
        hasCompletedTravelToTown = questStepState == "true";
        UpdateState();
	}
}
