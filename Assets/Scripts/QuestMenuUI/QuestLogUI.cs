using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using System.Linq;

public class QuestLogUI : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] private GameObject contentParent;
    [SerializeField] private GameObject closedBookToggle;
    [SerializeField] private QuestLogScrollingList scrollingList;
    [SerializeField] private TextMeshProUGUI questDisplayNameText;
    [SerializeField] private TextMeshProUGUI questStatusText; // or state?
    [SerializeField] private TextMeshProUGUI rewardsTrustLevelText;
    [SerializeField] private TextMeshProUGUI requiredTrustLevelText;
    [SerializeField] private TextMeshProUGUI requiredPreviousLevelsText;
    [SerializeField] private InputActionReference toggleQuestLogAction; // Reference to the input action
    private Button firstSelectedButton;

    [SerializeField] private QuestManager questManager;

    private void OnEnable()
    {
        toggleQuestLogAction.action.performed += OnToggleQuestLog; // Bind the input action
        GameEventsManager.instance.dialogueEvents.onDialogueStart += OnDialogueStart;
        GameEventsManager.instance.dialogueEvents.onDialogueComplete += OnDialogueComplete;
        toggleQuestLogAction.action.Enable();
        questManager = QuestManager.instance;
    }
    private void OnDisable()
    {
        toggleQuestLogAction.action.performed -= OnToggleQuestLog; // Unbind the input action
        GameEventsManager.instance.dialogueEvents.onDialogueStart -= OnDialogueStart;
        GameEventsManager.instance.dialogueEvents.onDialogueComplete -= OnDialogueComplete;
        toggleQuestLogAction.action.Disable();
    }

    private void OnToggleQuestLog(InputAction.CallbackContext context)
    {
        if (contentParent.activeInHierarchy)
        {
            HideUI();
            closedBookToggle.SetActive(true);
        }
        else
        {
            ShowUI();
            closedBookToggle.SetActive(false);
        }
    }
    
    private void OnDialogueStart()
    {
        closedBookToggle.SetActive(false);
    }
    
    private void OnDialogueComplete()
    {
        closedBookToggle.SetActive(true);
    }

    private void ShowUI()
    {
        contentParent.SetActive(true);
        GameEventsManager.instance.playerEvents.DisablePlayerMovement();

        firstSelectedButton = null;

        List<Quest> allQuests = QuestManager.instance.questMap.Values.ToList();
        foreach (Quest quest in allQuests)
        {
            Quest q = quest;
            QuestStateChange(q);
        }

        if (firstSelectedButton != null)
        {
            firstSelectedButton.Select();
        }
    }
    private void HideUI()
    {
        contentParent.SetActive(false);
        GameEventsManager.instance.playerEvents.EnablePlayerMovement();
        EventSystem.current.SetSelectedGameObject(null);
    }

    private void QuestStateChange(Quest quest)
    {
        QuestLogButton questLogButton = scrollingList.CreateButtonIfNotExists(quest, () => {
            SetQuestLogInfo(quest);
        });

        if (firstSelectedButton == null)
        {
            firstSelectedButton = questLogButton.button;
        }
    }
    private void SetQuestLogInfo(Quest quest)
    {
        // quest name
        questDisplayNameText.text = quest.questInfoSO.displayName;
        questStatusText.text = quest.GetFullStatusText();

        // requirements
        requiredTrustLevelText.text = "Trust Level " + quest.questInfoSO.requiredTrustLevel.ToString();
        requiredPreviousLevelsText.text = "";
        foreach (QuestInfoSO prereqQuestInfo in quest.questInfoSO.requiredPreviousQuests)
        {
            requiredPreviousLevelsText.text += prereqQuestInfo.displayName + "\n";
        }

        // rewards
        rewardsTrustLevelText.text = "Trust Level " + quest.questInfoSO.rewardTrustLevel.ToString();
    }
}