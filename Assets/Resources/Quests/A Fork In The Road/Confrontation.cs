using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
public class Confrontation : QuestStep
{
    private Quest quest;
    private bool hasMovedToWaypoint;
    private bool isPlayerInRange;
    private GameObject visualIndicatorObject;
    private SpriteRenderer visualIndicator;
    private string dialogueKnotName = "companion_confrontation";

    private PlayerControls inputActions;

    private void Awake()
    {
        hasMovedToWaypoint = false;
        isPlayerInRange = false;
        inputActions = new PlayerControls();
        inputActions.Enable();
    }

	private void OnDestroy()
	{
        GameEventsManager.instance.dialogueEvents.onDialogueStart -= OnDialogueStart;
        GameEventsManager.instance.dialogueEvents.onDialogueComplete -= OnDialogueComplete;
        inputActions.Disable();
	}

    private void Start()
    {
        visualIndicatorObject = this.gameObject.transform.GetChild(0).gameObject;
        visualIndicator = visualIndicatorObject.GetComponent<SpriteRenderer>();    
    
        quest = QuestManager.instance.GetQuestById(base.questID);
        
        GameEventsManager.instance.dialogueEvents.onDialogueStart += OnDialogueStart;
        GameEventsManager.instance.dialogueEvents.onDialogueComplete += OnDialogueComplete;
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
            Vector3 targetPosition = new Vector3(-74.42f, 13.73f, 134.43f);
            StartCoroutine(WorldSaveGameManager.instance.LoadSceneInGame(7, targetPosition));
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
        string status = state == "true" ? "Your companions have confronted you." : "Go reflect by yourself.";
        ChangeState("", status);
    }

    protected override void SetQuestStepState(string state)
    {
        
    }
}