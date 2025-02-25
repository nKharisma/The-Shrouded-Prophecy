using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class SkillCheckManager : MonoBehaviour
{
    [Header("Skill Check UI")]
    [SerializeField] private GameObject skillCheckUI;

    private DialogueManager dialogueManager;

    public bool skillCheckUIIsPlaying { get; private set; }

    void Start()
    {
        skillCheckUIIsPlaying = false;
        skillCheckUI.SetActive(false);

        dialogueManager = FindObjectOfType<DialogueManager>();
    }

    public void playSkillCheckUI()
    {
        skillCheckUIIsPlaying = true;
        skillCheckUI.SetActive(true);

        dialogueManager.PauseDialogue();
    }

    public void endConditions(int condition)
    {
        // survived the clock
        if (condition == 1)
        {
            skillCheckUIIsPlaying = false;
            skillCheckUI.SetActive(false);
        }

        // ran out of health
        if (condition == 2)
        {
            skillCheckUIIsPlaying = false;
            skillCheckUI.SetActive(false);
        }

        dialogueManager.UnPauseDialogue();
    }
}
