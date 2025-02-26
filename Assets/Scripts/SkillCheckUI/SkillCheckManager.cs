using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading.Tasks;
using UnityEngine.EventSystems;

public class SkillCheckManager : MonoBehaviour
{
    [Header("Skill Check UI")]
    [SerializeField] private GameObject skillCheckUI;

    private DialogueManager dialogueManager;

    public int conditionResult = 0;
    public bool skillCheckUIIsPlaying { get; private set; }

    void Start()
    {
        skillCheckUIIsPlaying = false;
        skillCheckUI.SetActive(false);

        dialogueManager = FindObjectOfType<DialogueManager>();
    }

    public async Task playSkillCheckUI()
    {
        skillCheckUIIsPlaying = true;
        skillCheckUI.SetActive(true);

        dialogueManager.PauseDialogue();

        while (skillCheckUIIsPlaying)
        {
            await Task.Yield();
        }
    }

    public void endConditions(int condition)
    {
        conditionResult = condition;

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

        // unpause dialogue
        dialogueManager.UnPauseDialogue();
    }
}
