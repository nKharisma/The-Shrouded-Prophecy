using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
public class QuestPoint : MonoBehaviour
{

    [Header("Dialogue")]
    [SerializeField] private string dialogueKnotName;


    [Header("Quest Info")]
    private bool playerInRange = false;
    private string questID;
    
    [SerializeField] private QuestInfoSO questInfoForPoint;
    
    private QuestState currentQuestState;
    
    private QuestIcon questIcon;
    
    private PlayerControls inputActions;
    
    [Header("Start/Finish")]
    [SerializeField] private bool startPoint;
    [SerializeField] private bool completePoint;
    
    private void Awake() 
    {
        questID = questInfoForPoint.questID;
        questIcon = GetComponentInChildren<QuestIcon>(true);
        
        inputActions = new PlayerControls();
        inputActions.Enable();
        
        Debug.Log("Quest Point Awake: " + questID);
        
    }
    
    private void Start() {
        ManageQuestPointState();
    }
    
    private void OnEnable()    
    {
        GameEventsManager.instance.questEvents.onQuestStateChange += QuestStateChange;
    }
    
    private void OnDisable() 
    {
        GameEventsManager.instance.questEvents.onQuestStateChange -= QuestStateChange;
    }
    
    private void Update() {
        if(inputActions.PlayerMovement.NPCInteraction.WasPressedThisFrame() && inputActions != null && playerInRange)
        {
            NPCInteractionButton();
        }
    }
    
    private void NPCInteractionButton()
    {
        if(!playerInRange)
        {
            return;
        }
        
        if(!dialogueKnotName.Equals(""))
        {
            GameEventsManager.instance.dialogueEvents.EnterDialogue(dialogueKnotName);
        }else {
            if (currentQuestState.Equals(QuestState.Can_Start) && startPoint)
            {
                GameEventsManager.instance.questEvents.StartQuest(questID);
            }
            else if (currentQuestState.Equals(QuestState.Can_Complete) && completePoint)
            {
                GameEventsManager.instance.questEvents.CompleteQuest(questID);
            }
        }
    }
    
    private void QuestStateChange(Quest quest)
    {
        if(quest.questInfoSO.questID.Equals(questID))
        {
            currentQuestState = quest.questState;
            Debug.Log("Quest State Changed: " + quest.questState + " for Quest: " + questID);
            questIcon.SetState(currentQuestState, startPoint, completePoint);
            ManageQuestPointState();
        }
    }
    
    private void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.CompareTag("Player"))
        {
            playerInRange = true;
        }
    }
    
    private void OnTriggerExit(Collider collider)
    {
        if (collider.gameObject.CompareTag("Player"))
        {
            playerInRange = false;
        }
    }
    
    private void ManageQuestPointState()
    {
        if(currentQuestState.Equals(QuestState.Can_Complete) && completePoint)
        {
            SetQuestPointActive(true);
        }else if(currentQuestState.Equals(QuestState.Can_Start) && startPoint)
        {
            SetQuestPointActive(true);
        }
    }
    
    private void SetQuestPointActive(bool isActive)
    {
        this.gameObject.GetComponent<BoxCollider>().enabled = isActive;
        questIcon.gameObject.SetActive(isActive);
    }
}
