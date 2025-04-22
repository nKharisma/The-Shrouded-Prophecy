using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(BoxCollider))]
public class GetAspenToTheMedic : QuestStep
{
    private Quest quest;
    private bool hasTalkedToMedic;
    private bool isPlayerInRange;
    private GameObject visualIndicatorObject;
    private SpriteRenderer visualIndicator;
    private GameObject mageNPC;
    private string dialogueKnotName = "medic_mage";

    private PlayerControls inputActions;

    private void Awake()
    {
        hasTalkedToMedic = false;
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
        mageNPC = GameObject.FindWithTag("Mage");
        if (mageNPC == null)
        {
            Debug.LogError("Mage NPC not found.");
            return;
        }

        visualIndicatorObject = mageNPC.transform.GetChild(0).gameObject;
        visualIndicator = visualIndicatorObject.GetComponent<SpriteRenderer>();
        quest = QuestManager.instance.GetQuestById(base.questID);

        Debug.Log("Initialization complete.");
    }

    private void Update()
    {
        if (isPlayerInRange && !hasTalkedToMedic)
        {
            if (inputActions.PlayerMovement.NPCInteraction.WasPressedThisFrame())
            {
                DialogueManager.GetInstance().EnterDialogue(dialogueKnotName);
            }
        }

        if (quest != null && quest.currentQuestStepIndex == base.currentStepIndex && !hasTalkedToMedic)
        {
            if (visualIndicator != null)
            {
                visualIndicator.enabled = true;
            }
        }
    }

    private void OnDialogueStart()
    {
        if (hasTalkedToMedic)
        {
            return;
        }

        if (DialogueManager.GetInstance().currentKnotName == dialogueKnotName)
        {
            hasTalkedToMedic = true;
            visualIndicator.enabled = false;
        }
    }

    private void OnDialogueComplete()
    {
        if (hasTalkedToMedic)
        {
            if (visualIndicator != null)
            {
                visualIndicator.enabled = false;
            }
            //Debug.Log("Dialogue complete. Player has talked to the companion.");
            UpdateState();
            CompleteStep();
            Vector3 targetPosition = new Vector3(-76.68f, 13.73f, 130.4f);
            StartCoroutine(WorldSaveGameManager.instance.LoadSceneInGame(6, targetPosition));
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
        string state = hasTalkedToMedic ? "true" : "false";
        string status = state == "true" ? "You have delivered Aspen to the mage medic." : "Get Aspen to the medic.";
        ChangeState("", status);
    }

    protected override void SetQuestStepState(string state)
    {
        
    }
}
