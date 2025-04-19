using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArriveAtTheTownSquare : QuestStep
{
    private Quest quest;
    private bool isPlayerInRange;
    private bool isTownSquareReached;
    private PlayerControls inputActions;
    private GameObject wayPoint;
    private BoxCollider wayPointCollider;
    
    private string dialogueKnotName = "town_square";

	private void Awake()
	{
		isPlayerInRange = false;
		isTownSquareReached = false;
		inputActions = new PlayerControls();
		inputActions.Enable();
	}

	private void Start()
	{
		wayPoint = GameObject.Find("TownSquareCheckpoint");
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
		if(isTownSquareReached || !isPlayerInRange)
		{
			return;
		}
		
		
		if(DialogueManager.GetInstance().currentKnotName == dialogueKnotName)
		{
			isTownSquareReached = true;
		}
	}
	
	private void OnDialogueComplete()
	{
		if(isTownSquareReached)
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
		string state = isTownSquareReached ? "true" : "false";
		string status = "";
		if(state == "true")
		{
			status = "You have arrived to the town square.";
		}
		else
		{
			status = "Go visit the town center.";
		}
		ChangeState("", status);
	}

	protected override void SetQuestStepState(string questStepState)
	{
		
	}
}
