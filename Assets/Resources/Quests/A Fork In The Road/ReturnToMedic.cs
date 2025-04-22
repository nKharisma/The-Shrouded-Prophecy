using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
public class ReturnToMedic : QuestStep
{
    private Quest quest;
    private bool isPlayerInRange;
    private bool hasCompletedTravelToTown;
    private PlayerControls inputActions;
    private GameObject aspenNPC;
    private GameObject visualIndicatorObject;
    private SpriteRenderer visualIndicator;
    private string dialogueKnotName = "travel_to_town";
    
    private void Awake() {
        isPlayerInRange = false;
        hasCompletedTravelToTown = false;
        inputActions = new PlayerControls();
        inputActions.Enable();
    }
    
    private void Start() {
        aspenNPC = GameObject.FindWithTag("Immunity");
        if(aspenNPC == null)
        {
            Debug.Log("Aspen NPC not found");
        }
        
        visualIndicatorObject = aspenNPC.transform.GetChild(0).gameObject;
        visualIndicator = visualIndicatorObject.GetComponent<SpriteRenderer>();
    
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
            visualIndicator.enabled = true;
        }
	}
	
	private void OnDialogueStart()
	{
	    if(hasCompletedTravelToTown)
        {
            return;
        }
        
        if(DialogueManager.GetInstance().currentKnotName == dialogueKnotName)
        {
            visualIndicator.enabled = false;
            hasCompletedTravelToTown = true;
            Debug.Log("Player has traveled to the town");
        }
	}
	
	private void OnDialogueComplete()
	{
        if(hasCompletedTravelToTown){
        //miraCollider.enabled = false;
        UpdateState();
        //here to transfer the player to the town
        Debug.Log("Player has completed the travel to the town");
        CompleteStep();
        StartCoroutine(WorldSaveGameManager.instance.LoadSceneInGame(5, new Vector3(-90.4f, 5.16f, 79.31f)));
        }
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
            status = "Help Mira get to the medic.";
        }
        ChangeState("", status);
	}

	protected override void SetQuestStepState(string questStepState)
	{
        
	}
}
