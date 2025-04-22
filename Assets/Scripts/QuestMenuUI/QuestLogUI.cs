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
        toggleQuestLogAction.action.Enable();
    }
    private void OnDisable()
    {
        toggleQuestLogAction.action.performed -= OnToggleQuestLog; // Unbind the input action
        toggleQuestLogAction.action.Disable();
    }

    private void OnToggleQuestLog(InputAction.CallbackContext context)
    {
        if (contentParent.activeInHierarchy)
        {
            HideUI();
        }
        else
        {
            ShowUI();
        }
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