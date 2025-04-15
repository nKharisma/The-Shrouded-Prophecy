using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class DialogueTrigger : MonoBehaviour
{
    [Header("Visual Cue")]
    [SerializeField] private GameObject visualCue;

    [Header("Ink JSON")]
    [SerializeField] private TextAsset inkJSON;

    private bool playerInRange;
    private PlayerControls inputActions;

    //public UnityEvent OnDialogueComplete;
    
    [SerializeField] private bool autoTriggerDialogue = false;
    private void Awake()
    {
        playerInRange = false;
                
        if(visualCue != null)
        {
            visualCue.SetActive(false);
        }
        
        if(inputActions == null)
        {
            inputActions = new PlayerControls();
        }
        inputActions.Enable();

    }
    
    private void OnDestroy()
    {
        if (inputActions != null)
        {
            inputActions.Disable();
        }
    }

    private void Update()
    {
        if(!autoTriggerDialogue)
        {
            IfDialogueTrigger();
        }
    }
    
    private void IfDialogueTrigger()
    {
        if (playerInRange && !DialogueManager.GetInstance().dialogueIsPlaying)
        {
            if(visualCue != null)
            {
                visualCue.SetActive(true);
            }

            if (inputActions != null && inputActions.PlayerMovement.NPCInteraction.WasPressedThisFrame())
            {
                DialogueManager.GetInstance().EnterDialogueMode(inkJSON);
                //GameEventsManager.instance.dialogueEvents.onDialogueComplete += GameEventsManager.instance.dialogueEvents.DialogueComplete;
            }
        }
        else
        {
            if (visualCue != null)
            {
                visualCue.SetActive(false);
            }
        }
    }

    private void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.CompareTag("Player"))
        {
            playerInRange = true;
            
            if (autoTriggerDialogue && !DialogueManager.GetInstance().dialogueIsPlaying)
            {
                DialogueManager.GetInstance().EnterDialogueMode(inkJSON);
                autoTriggerDialogue = false;
            }
        }
    }

    private void OnTriggerExit(Collider collider)
    {
        if (collider.gameObject.CompareTag("Player"))
        {
            playerInRange = false;
        }
    }
}
