using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoToTheMedTent : QuestStep
{
    private Quest quest;
    private bool isPlayerInRange;
    private bool isAtHealerTent;
    private PlayerControls inputActions;
    private GameObject wayPoint;
    private SpriteRenderer spriteRenderer;
    
    private string dialogueKnotName = "healers_tent";

	private void Awake()
	{
		isPlayerInRange = false;
		isAtHealerTent = false;
		inputActions = new PlayerControls();
		inputActions.Enable();
	}

	private void Start()
	{
		wayPoint = GameObject.Find("MedTentCheckpoint");
		if (wayPoint == null)
		{
			Debug.Log("WayPoint not found");
			return;
		}
		
		spriteRenderer = wayPoint.GetComponent<SpriteRenderer>();
		
		quest = QuestManager.instance.GetQuestById(base.questID);
		
		GameEventsManager.instance.dialogueEvents.onDialogueStart += OnDialogueStart;
		GameEventsManager.instance.dialogueEvents.onDialogueComplete += OnDialogueComplete;
	}
	
	private void Update()
	{
		if(!isAtHealerTent && isPlayerInRange)
		{
			DialogueManager.GetInstance().EnterDialogue(dialogueKnotName);
		}
		
		if(quest.currentQuestStepIndex == base.currentStepIndex)
		{
			spriteRenderer.enabled = true;
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
		if(isAtHealerTent || !isPlayerInRange)
		{
			return;
		}
		
		
		if(DialogueManager.GetInstance().currentKnotName == dialogueKnotName)
		{
			isAtHealerTent = true;
			spriteRenderer.enabled = false;
		}
	}
	
	private void OnDialogueComplete()
	{
		if(isAtHealerTent)
		{
			UpdateState();
			
			if(wayPoint != null)
			{
				Destroy(wayPoint);
			}
			CompleteStep();
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
		string state = isAtHealerTent ? "true" : "false";
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
