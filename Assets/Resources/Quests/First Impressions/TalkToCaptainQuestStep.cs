using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
public class TalkToCaptainQuestStep : QuestStep
{
    private bool isTalkingToCaptain;
    private bool isPlayerInRange;
    private GameObject captainNPC;
    private string dialogueKnotName = "CaptainDialogue";
    private PlayerControls inputActions;

    private void Awake() {
        isTalkingToCaptain = false;
        isPlayerInRange = false;
        inputActions = new PlayerControls();
        inputActions.Enable();
    }

    private void Start()
    {   
        captainNPC = GameObject.FindWithTag("Captain");
    
        if(captainNPC == null)
        {
            Debug.Log("Captain NPC not found");
            return;
        }
    
        GameEventsManager.instance.dialogueEvents.onDialogueComplete += OnDialogueStart;
        GameEventsManager.instance.dialogueEvents.onDialogueComplete += OnDialogueComplete;
    }
    
    private void Update()
    {
        if(isPlayerInRange && !isTalkingToCaptain)
        {
            if(inputActions.PlayerMovement.NPCInteraction.WasPressedThisFrame())
            {
                DialogueManager.GetInstance().EnterDialogue(dialogueKnotName);
            }
        }
    }

    private void OnDestroy()
    {
        GameEventsManager.instance.dialogueEvents.onDialogueComplete -= OnDialogueStart;
        GameEventsManager.instance.dialogueEvents.onDialogueComplete -= OnDialogueComplete;
    }
    
    private void OnDialogueStart()
    {
        if(isTalkingToCaptain)
        {
            return;
        }
        
        if(DialogueManager.GetInstance().currentKnotName == dialogueKnotName)
        {
            isTalkingToCaptain = true;
        }
    }
    
    private void OnDialogueComplete()
    {
        if(isTalkingToCaptain)
        {
            isTalkingToCaptain = false;
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
}
