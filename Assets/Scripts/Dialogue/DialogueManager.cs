using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Ink.Runtime;
using UnityEngine.EventSystems;

//might seperate the dialoguepanelUI and dialogue choices into their own scripts later
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

    [Header("Ink Story")]
    [SerializeField] private TextAsset inkJson;
    
    private Story currentStory;
    private Story questStory;

    public bool dialogueIsPlaying { get; private set; }
    public bool isPaused { get; private set; }

    private static DialogueManager instance;
    private bool isQuestDialogue = false;
    
    public string currentKnotName { get; private set; }
    private PlayerControls inputActions;
    private SkillCheckManager skillCheck;
    private InkExternalFunctions inkExternalFunctions;

    private Vector3 noChoicePosition = new Vector3(0f, 10f, 0f);
    private Vector3 choicePosition = new Vector3(0f, 25f, 0f);
    private RectTransform panelRect;
    
    private string lastSelectedChoiceText;

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
        
        questStory = new Story(inkJson.text);
        inkExternalFunctions = new InkExternalFunctions();
        inkExternalFunctions.Bind(questStory);

        panelRect = dialoguePanel.GetComponent<RectTransform>();
        iconRect = continueIcon.GetComponent<RectTransform>();
    }
    

    public static DialogueManager GetInstance()
    {
        return instance;
    }
    
    private void OnEnable() 
    {
        GameEventsManager.instance.dialogueEvents.onEnterDialogue += EnterDialogue;
        GameEventsManager.instance.dialogueEvents.onExitDialogue += ExitDialogue;
        GameEventsManager.instance.dialogueEvents.onDialogueStart += DialogueStart;
        GameEventsManager.instance.dialogueEvents.onDialogueComplete += DialogueComplete;
        GameEventsManager.instance.dialogueEvents.onDisplayDialogue += DisplayDialogue;
        if(inputActions == null)
        {
            inputActions = new PlayerControls();
        }
        inputActions.Enable();
    }
    
    private void OnDisable()
    {
        GameEventsManager.instance.dialogueEvents.onEnterDialogue -= EnterDialogue;
        GameEventsManager.instance.dialogueEvents.onExitDialogue -= ExitDialogue;
        GameEventsManager.instance.dialogueEvents.onDialogueStart -= DialogueStart;
        GameEventsManager.instance.dialogueEvents.onDialogueComplete -= DialogueComplete;
        GameEventsManager.instance.dialogueEvents.onDisplayDialogue -= DisplayDialogue;
        inkExternalFunctions.Unbind(questStory);
        inputActions.Disable();
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
            if(isQuestDialogue)
            {
                ContinueOrExitStory();
            }else {
                ContinueStory();
            }
        }
    }

    public void EnterDialogueMode(TextAsset inkJSON)
    {
        currentStory = new Story(inkJSON.text);
        
        Debug.Log("Current story is: " + currentStory);
        dialogueIsPlaying = true;
        dialoguePanel.SetActive(true);

        currentStory.BindExternalFunction("playSkillCheckUI", async () => {
            await skillCheck.playSkillCheckUI();
            currentStory.variablesState["result"] = skillCheck.conditionResult;
        });

        ContinueStory();
    }
    
    public void EnterDialogue(string knotName)
    {
        
        if(dialogueIsPlaying)
        {
            return;
        }
        
        dialogueIsPlaying = true;
        isQuestDialogue = true;
                
        if(!knotName.Equals(""))
        {
            questStory.ChoosePathString(knotName);
            currentKnotName = knotName;
            GameEventsManager.instance.dialogueEvents.DialogueStart();
        }else{
            Debug.Log("Knot name is empty");
        }
        
        questStory.BindExternalFunction("playSkillCheckUI", async () => {
            await skillCheck.playSkillCheckUI();
            questStory.variablesState["result"] = skillCheck.conditionResult;
        });
        
        ContinueOrExitStory();
    }
    
    private void ExitDialogue()
    {
        Debug.Log("Exiting Dialogue");
        
        dialogueIsPlaying = false;
        isQuestDialogue = false;
        
        GameEventsManager.instance.dialogueEvents.DialogueComplete();
        questStory.UnbindExternalFunction("playSkillCheckUI");
        questStory.ResetState();
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
        currentStory.ResetState();
    }
    
    private void DialogueStart()
    {
        dialoguePanel.SetActive(true);
    }
    
    private void DialogueComplete()
    {
        dialoguePanel.SetActive(false);
        dialogueText.text = "";
    }
    
    private void DisplayDialogue(string dialogue)
    {
        dialogueText.text = dialogue;
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
    
    private void ContinueOrExitStory()
    {
        if (questStory.canContinue)
        {
            string dialogueLine = questStory.Continue();
            GameEventsManager.instance.dialogueEvents.DisplayDialogue(dialogueLine);
            DisplayChoices();
            
            while(IsDialogueEmpty(dialogueLine) && questStory.canContinue)
            {
                dialogueLine = questStory.Continue();
            }
            
            if(IsDialogueEmpty(dialogueLine) && !questStory.canContinue)
            {
                ExitDialogue();
            }else {
                GameEventsManager.instance.dialogueEvents.DisplayDialogue(dialogueLine);
            }
        }else if(questStory.currentChoices.Count == 0)
        {
            ExitDialogue();
        }
    }

    private void DisplayChoices()
    {
    List<Choice> currentChoices = isQuestDialogue ? questStory.currentChoices : currentStory.currentChoices;

    if (currentChoices.Count > choices.Length)
    {
        Debug.Log("More choices were given than UI can support");
    } 

    int index = 0;
    foreach (Choice choice in currentChoices)
    {
        choices[index].gameObject.SetActive(true);
        choicesText[index].text = choice.text;
        index++;
    }

    for (int i = index; i < choices.Length; i++)
    {
        choices[i].gameObject.SetActive(false);
    }

    // Adjust the panel and icon positions based on the number of choices
    if (currentChoices.Count > 0)
    {
        panelRect.anchoredPosition = choicePosition;
        iconRect.anchoredPosition = downPosition;
        iconRect.transform.rotation = Quaternion.Euler(0, 0, 0);
    }
    else
    {
        panelRect.anchoredPosition = noChoicePosition;
        iconRect.anchoredPosition = sidePosition;
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
        Story story = isQuestDialogue ? questStory : currentStory;
        
        lastSelectedChoiceText = story.currentChoices[choiceIndex].text;
        
        story.ChooseChoiceIndex(choiceIndex);
    }
    
    public string GetLastSelectedChoice()
    {
        return lastSelectedChoiceText;
    }
    
    private bool IsDialogueEmpty(string dialogueLine)
    {
        return dialogueLine.Trim().Equals("") || dialogueLine.Trim().Equals("\n");
    }
}
