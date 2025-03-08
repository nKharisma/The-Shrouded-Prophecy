using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading.Tasks;
using UnityEngine.EventSystems;

public class SkillCheckManager : MonoBehaviour
{
    public static SkillCheckManager instance { get; private set; }

    [SerializeField] private GameObject skillCheckUIInstance;
    private DialogueManager dialogueManager;

    public int conditionResult = 0;
    public bool skillCheckUIIsPlaying { get; private set; }

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        skillCheckUIIsPlaying = false;
        dialogueManager = FindObjectOfType<DialogueManager>();
    }

    public void SetSkillCheck(GameObject skillCheckUI)
    {
        if (skillCheckUI == null)
        {
            Debug.LogError("SkillCheckUI is not set.");
            return;
        }

        skillCheckUIInstance = skillCheckUI;
        skillCheckUIInstance.SetActive(false);
    }

    public async Task playSkillCheckUI()
    {

        skillCheckUIIsPlaying = true;
        skillCheckUIInstance.SetActive(true);

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
            skillCheckUIInstance.SetActive(false);
        }

        // ran out of health
        if (condition == 2)
        {
            skillCheckUIIsPlaying = false;
            skillCheckUIInstance.SetActive(false);
        }

        // unpause dialogue
        dialogueManager.UnPauseDialogue();
    }
}