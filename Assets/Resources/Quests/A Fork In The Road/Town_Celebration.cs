using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(BoxCollider))]
public class Town_Celebration : QuestStep
{
    private Quest quest;
    private bool hasMovedToWaypoint;
    private bool isPlayerInRange;
    private GameObject visualIndicatorObject;
    private SpriteRenderer visualIndicator;
    private string dialogueKnotName = "town_celebration";

    private PlayerControls inputActions;

    private void Awake()
    {
        hasMovedToWaypoint = false;
        isPlayerInRange = false;
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
        visualIndicatorObject = this.gameObject.transform.GetChild(0).gameObject;
        visualIndicator = visualIndicatorObject.GetComponent<SpriteRenderer>();    
    
        quest = QuestManager.instance.GetQuestById(base.questID);

        Debug.Log("Initialization complete.");
    }

    private void Update()
    {
        if (isPlayerInRange && !hasMovedToWaypoint)
        {
            DialogueManager.GetInstance().EnterDialogue(dialogueKnotName);
        }
    }

    private void OnDialogueStart()
    {
        if (hasMovedToWaypoint)
        {
            return;
        }

        if (DialogueManager.GetInstance().currentKnotName == dialogueKnotName)
        {
            hasMovedToWaypoint = true;
            visualIndicator.enabled = false;
        }
    }

    private void OnDialogueComplete()
    {
        if (hasMovedToWaypoint)
        {
            //Debug.Log("Dialogue complete. Player has talked to the companion.");
            UpdateState();
            CompleteStep();
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
        string state = hasMovedToWaypoint ? "true" : "false";
        string status = state == "true" ? "You have moved to the town celebration." : "Move to the town center.";
        ChangeState("", status);
    }

    protected override void SetQuestStepState(string state)
    {
        
    }
}
