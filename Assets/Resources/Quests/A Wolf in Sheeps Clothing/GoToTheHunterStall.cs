using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoToTheHunterStall : QuestStep
{
    private Quest quest;
    private bool isPlayerInRange;
    private bool isAtHunterStall;
    private PlayerControls inputActions;
    private GameObject wayPoint;
    private BoxCollider wayPointCollider;
    
    private string dialogueKnotName = "hunters_tent";

	private void Awake()
	{
		isPlayerInRange = false;
		isAtHunterStall = false;
		inputActions = new PlayerControls();
		inputActions.Enable();
	}

	private void Start()
	{
		wayPoint = GameObject.Find("HunterStallCheckpoint");
		if (wayPoint == null)
		{
			Debug.Log("WayPoint not found");
			return;
		}
		
		wayPointCollider = wayPoint.GetComponent<BoxCollider>();
		
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
	
	private void OnDialogueStart()
	{
		if(isAtHunterStall || !isPlayerInRange)
		{
			return;
		}
		
		
		if(DialogueManager.GetInstance().currentKnotName == dialogueKnotName)
		{
			isAtHunterStall = true;
		}
	}
	
	private void OnDialogueComplete()
	{
		if(isAtHunterStall)
		{
			UpdateState();
			CompleteStep();
			
			if(wayPoint != null)
			{
				Destroy(wayPoint);
			}
		}
	}

	void OnTriggerEnter(Collider other)
	{
		if(other.CompareTag("Player"))
		{
			isPlayerInRange = true;
		}
	}
	
	void OnTriggerExit(Collider other)
	{
		if(other.CompareTag("Player"))
		{
			isPlayerInRange = false;
		}
	}
	
	private void UpdateState()
	{
		string state = isAtHunterStall ? "true" : "false";
		string status = "";
		if(state == "true")
		{
			status = "You have arrived at the hunter's stall";
		}
		else
		{
			status = "Go visit the hunter's stall.";
		}
		ChangeState("", status);
	}

	protected override void SetQuestStepState(string questStepState)
	{
		
	}
}
