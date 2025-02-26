using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Ink.Runtime;
using UnityEngine.EventSystems;

public class DialogueManager : MonoBehaviour
{
    [Header("Dialogue UI")]
    [SerializeField] private GameObject dialoguePanel;
    [SerializeField] private TextMeshProUGUI dialogueText;

    [Header("Choices UI")]
    [SerializeField] private GameObject[] choices;

    [Header("Continue Icon")]
    [SerializeField] private GameObject continueIcon;

    private TextMeshProUGUI[] choicesText;

    private Story currentStory;

    public bool dialogueIsPlaying { get; private set; }
    public bool isPaused { get; private set; }

    private static DialogueManager instance;
    private PlayerControls inputActions;
    private SkillCheckManager skillCheck;

    private Vector3 noChoicePosition = new Vector3(0f, 20f, 0f);
    private Vector3 choicePosition = new Vector3(0f, 82f, 0f);
    private RectTransform panelRect;

    private Vector3 downPosition = new Vector3(0f, 5f, 0f);
    private Vector3 sidePosition = new Vector3(340f, 13f, 0f);
    private RectTransform iconRect;

    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogWarning("More than one Dialogue Manager in this scene");
        }

        instance = this;

        panelRect = dialoguePanel.GetComponent<RectTransform>();
        iconRect = continueIcon.GetComponent<RectTransform>();

        inputActions = new PlayerControls();
        inputActions.Enable();
    }

    public static DialogueManager GetInstance()
    {
        return instance;
    }

    private void Start()
    {
        isPaused = false;
        dialogueIsPlaying = false;
        dialoguePanel.SetActive(false);

        choicesText = new TextMeshProUGUI[choices.Length];
        skillCheck = FindObjectOfType<SkillCheckManager>();

        int index = 0;
        foreach (GameObject choice in choices)
        {
            choicesText[index] = choice.GetComponentInChildren<TextMeshProUGUI>();
            index++;
        }
    }

    private void Update()
    {
        if (!dialogueIsPlaying || isPaused)
        {
            return;
        }

        // handle continuing to next line in the dialogue when submit is pressed
        if (inputActions != null && inputActions.PlayerMovement.NextDialogue.WasPressedThisFrame())
        {
            ContinueStory();
        }
    }

    public void EnterDialogueMode(TextAsset inkJSON)
    {
        currentStory = new Story(inkJSON.text);
        dialogueIsPlaying = true;
        dialoguePanel.SetActive(true);

        currentStory.BindExternalFunction("playSkillCheckUI", async () => {
            await skillCheck.playSkillCheckUI();
            currentStory.variablesState["result"] = skillCheck.conditionResult;
        });

        ContinueStory();
    }

    public void PauseDialogue()
    {
        isPaused = true;
    }

    public void UnPauseDialogue()
    {
        isPaused = false;
    }

    private void ExitDialogueMode()
    {
        dialogueIsPlaying = false;
        dialoguePanel.SetActive(false);
        dialogueText.text = "";

        currentStory.UnbindExternalFunction("playSkillCheckUI");
    }

    private void ContinueStory()
    {
        if (currentStory.canContinue)
        {
            dialogueText.text = currentStory.Continue();
            DisplayChoices();
        }
        else
        {
            ExitDialogueMode();
        }
    }

    private void DisplayChoices()
    {
        List<Choice> currentChoices = currentStory.currentChoices;

        if (currentChoices.Count > choices.Length)
        {
            Debug.Log("More choices were given than UI can support");
        } 

        int index = 0;
        foreach (Choice choice in currentChoices)
        {
            iconRect.transform.rotation = Quaternion.Euler(0, 0, 0);
            panelRect.anchoredPosition = choicePosition;
            iconRect.anchoredPosition = downPosition;
            choices[index].gameObject.SetActive(true);
            choicesText[index].text = choice.text;
            index++;
        }

        for (int i = index; i < choices.Length; i++)
        {
            choices[i].gameObject.SetActive(false);
            iconRect.anchoredPosition = sidePosition;
            panelRect.anchoredPosition = noChoicePosition;
            iconRect.transform.rotation = Quaternion.Euler(0, 0, 90);
        }

        StartCoroutine(SelectFirstChoice());
    }

    private IEnumerator SelectFirstChoice()
    {
        EventSystem.current.SetSelectedGameObject(null);
        yield return new WaitForEndOfFrame();
        EventSystem.current.SetSelectedGameObject(choices[0].gameObject);
    }

    public void MakeChoice(int choiceIndex)
    {
        currentStory.ChooseChoiceIndex(choiceIndex);
    }
}
