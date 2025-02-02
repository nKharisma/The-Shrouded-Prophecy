using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTrigger : MonoBehaviour
{
    [Header("Visual Cue")]
    [SerializeField] private GameObject visualCue;

    [Header("Ink JSON")]
    [SerializeField] private TextAsset inkJSON;

    private bool playerInRange;

    private void Awake()
    {
        playerInRange = false;
        visualCue.SetActive(false);
    }

    private void Update()
    {
        if (playerInRange)
        {
            visualCue.SetActive(true);
            // if (PlayerInputManager.GetInstance().GetInteractPressed())
            // {
            //     Debug.Log(inkJSON.text);
            // }
        }
        else
        {
            visualCue.SetActive(false);
        }
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        Debug.Log("Collided with: " + collider.gameObject.name);
        if (collider.gameObject.CompareTag("Player"))
        {
            Debug.Log("Player entered range");
            playerInRange = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collider)
    {
        Debug.Log("Exited trigger: " + collider.gameObject.name);
        if (collider.gameObject.CompareTag("Player"))
        {
            Debug.Log("Player exited range");
            playerInRange = false;
        }
    }
}
