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

    public UnityEvent OnDialogueComplete;
    private void Awake()
    {
        playerInRange = false;
        
        visualCue.SetActive(false);

        inputActions = new PlayerControls();
        inputActions.Enable();
    }

    private void Update()
    {
        IfDialogueTrigger();
    }
    
    private void IfDialogueTrigger()
    {
        if (playerInRange && !DialogueManager.GetInstance().dialogueIsPlaying)
        {
            visualCue.SetActive(true);

            if (inputActions != null && inputActions.PlayerMovement.NPCInteraction.WasPressedThisFrame())
            {
                DialogueManager.GetInstance().EnterDialogueMode(inkJSON);
                //GameEventsManager.instance.dialogueEvents.onDialogueComplete += GameEventsManager.instance.dialogueEvents.DialogueComplete;
            }
        }
        else
        {
            visualCue.SetActive(false);
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
}
