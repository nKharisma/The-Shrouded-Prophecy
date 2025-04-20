using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(BoxCollider))]
public class TalkToCompanionBeforeBattle : QuestStep
{
    private Quest quest;
    private bool hasTalkedToCompanion;
    private bool isPlayerInRange;
    private GameObject visualIndicatorObject;
    private SpriteRenderer visualIndicator;
    private GameObject companionNPC;
    private string dialogueKnotName = "talk_to_companion";

    private PlayerControls inputActions;
    [Header("Sprites")]
    [SerializeField] private Sprite questQuestionMark;

    private void Awake()
    {
        hasTalkedToCompanion = false;
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
        companionNPC = GameObject.FindWithTag("Healer");
        if (companionNPC == null)
        {
            Debug.LogError("Companion NPC not found.");
            return;
        }

        visualIndicatorObject = companionNPC.transform.GetChild(0).gameObject;
        visualIndicator = visualIndicatorObject.GetComponent<SpriteRenderer>();
        quest = QuestManager.instance.GetQuestById(base.questID);

        Debug.Log("Initialization complete.");
    }

    private void Update()
    {
        if (isPlayerInRange && !hasTalkedToCompanion)
        {
            if (inputActions.PlayerMovement.NPCInteraction.WasPressedThisFrame())
            {
                DialogueManager.GetInstance().EnterDialogue(dialogueKnotName);
            }
        }

        if (quest != null && quest.currentQuestStepIndex == base.currentStepIndex && !hasTalkedToCompanion)
        {
            if (visualIndicator != null)
            {
                visualIndicator.sprite = questQuestionMark;
            }
        }
    }

    private void OnDialogueStart()
    {
        if (hasTalkedToCompanion)
        {
            return;
        }

        if (DialogueManager.GetInstance().currentKnotName == dialogueKnotName)
        {
            hasTalkedToCompanion = true;
            visualIndicator.enabled = false;
            Debug.Log("Player has talked to the companion.");
        }
    }

    private void OnDialogueComplete()
    {
        if (hasTalkedToCompanion)
        {
            if (visualIndicator != null)
            {
                visualIndicator.sprite = questQuestionMark;
                visualIndicator.enabled = true;
            }
            //Debug.Log("Dialogue complete. Player has talked to the companion.");
            
            
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
        string state = hasTalkedToCompanion ? "true" : "false";
        string status = state == "true" ? "You have talked to the stranger." : "Talk to the stranger.";
        ChangeState("", status);
    }

    protected override void SetQuestStepState(string state)
    {
        
    }
}
