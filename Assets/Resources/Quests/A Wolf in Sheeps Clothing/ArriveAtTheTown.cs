using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArriveAtTheTown : QuestStep
{
    private Quest quest;
    private bool isPlayerInRange;
    private bool hasArrivedAtTown;
    private PlayerControls inputActions;
    private GameObject wayPoint;
    private BoxCollider wayPointCollider;
    
    private string dialogueKnotName = "town_arrival";

	private void Awake()
	{
		isPlayerInRange = false;
		hasArrivedAtTown = false;
		inputActions = new PlayerControls();
		inputActions.Enable();
	}

	private void Start()
	{
		wayPoint = GameObject.Find("TownArrivalCheckpoint");
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
		if(hasArrivedAtTown || !isPlayerInRange)
		{
			return;
		}
		
		
		if(DialogueManager.GetInstance().currentKnotName == dialogueKnotName)
		{
			hasArrivedAtTown = true;
		}
	}
	
	private void OnDialogueComplete()
	{
		if(hasArrivedAtTown)
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
		string state = hasArrivedAtTown ? "true" : "false";
		string status = "";
		if(state == "true")
		{
			status = "You have arrived to the town.";
		}
		else
		{
			status = "Arrive at the town.";
		}
		ChangeState("", status);
	}

	protected override void SetQuestStepState(string questStepState)
	{
		
	}
}
