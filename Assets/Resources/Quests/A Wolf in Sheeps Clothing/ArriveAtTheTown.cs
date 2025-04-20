using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(BoxCollider))]
public class ArriveAtTheTown : QuestStep
{
    private Quest quest;
    private bool isPlayerInRange;
    private bool hasArrivedAtTown;
    private PlayerControls inputActions;
    private GameObject wayPoint;
    private SpriteRenderer spriteRenderer;
    
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
        GameEventsManager.instance.dialogueEvents.onDialogueStart += OnDialogueStart;
        GameEventsManager.instance.dialogueEvents.onDialogueComplete += OnDialogueComplete;
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

	private void OnDestroy()
	{
		SceneManager.sceneLoaded -= OnSceneLoaded;
        GameEventsManager.instance.dialogueEvents.onDialogueStart -= OnDialogueStart;
        GameEventsManager.instance.dialogueEvents.onDialogueComplete -= OnDialogueComplete;
        inputActions.Disable();
	}

	private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        Initialize();
    }
    
    private void Initialize()
	{
		wayPoint = GameObject.Find("TownArrivalCheckpoint");
		if (wayPoint == null)
		{
			Debug.Log("WayPoint not found");
			return;
		}
		
		spriteRenderer = wayPoint.GetComponent<SpriteRenderer>();
		spriteRenderer.enabled = true;
		
		quest = QuestManager.instance.GetQuestById(base.questID);
	}

	private void Update()
	{
		if(!hasArrivedAtTown && isPlayerInRange)
		{
			DialogueManager.GetInstance().EnterDialogue(dialogueKnotName);
		}
	}
	
	private void OnDialogueStart()
	{
		if(hasArrivedAtTown)
		{
			return;
		}
		
		
		if(DialogueManager.GetInstance().currentKnotName == dialogueKnotName)
		{
			hasArrivedAtTown = true;
			spriteRenderer.enabled = false;
			Debug.Log("Arrived at the town.");
		}
	}
	
	private void OnDialogueComplete()
	{
		if(hasArrivedAtTown)
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
