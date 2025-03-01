using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestManager : MonoBehaviour
{
    private Dictionary<string, Quest> questMap;
    
    public static QuestManager instance;
    
    private int playerTrustLevel;
    
    private void Awake() {
    
        if(instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }else {
            Destroy(gameObject);
        }
    
        questMap = CreateQuestMap();
        
        Quest quest = GetQuestById("FirstImpressionsSO");
        Debug.Log("Quest State: " + quest.questState);
        Debug.Log(quest.GetCurrentStep());
    }
    
    private void OnEnable() {
        GameEventsManager.instance.questEvents.onStartQuest += StartQuest;
        GameEventsManager.instance.questEvents.onAdvanceQuest += AdvanceQuest;
        GameEventsManager.instance.questEvents.onCompleteQuest += CompleteQuest;
        
        GameEventsManager.instance.playerEvents.onPlayerTrustLevelChange += PlayerTrustLevelChange;
    }
    
    private void OnDisable() {
        GameEventsManager.instance.questEvents.onStartQuest -= StartQuest;
        GameEventsManager.instance.questEvents.onAdvanceQuest -= AdvanceQuest;
        GameEventsManager.instance.questEvents.onCompleteQuest -= CompleteQuest;
        
        GameEventsManager.instance.playerEvents.onPlayerTrustLevelChange += PlayerTrustLevelChange;
    }
    
    private void Start()
    {
        foreach(Quest quest in questMap.Values)
        {
            GameEventsManager.instance.questEvents.QuestStateChange(quest);
        }
    }
    
    private Quest GetQuestById(string id)
    {
        Debug.Log("GetQuestByID: " + id);
        Quest quest = questMap[id];
        
        if(quest == null)
        {
            Debug.LogWarning("Quest with ID not found: " + id);
        }
        return quest;
    }
    
    private void ChangeQuestState(string id, QuestState state)
    {
        Quest quest = GetQuestById(id);
        quest.questState = state;
        GameEventsManager.instance.questEvents.QuestStateChange(quest);
    }
    
    private void PlayerTrustLevelChange(int trustLevel)
    {
        playerTrustLevel = trustLevel;
    }
    
    private bool CheckRequirementsMet(Quest quest)
    {
        bool requirementsMet = true;
        
        if(playerTrustLevel < quest.questInfoSO.requiredTrustLevel)
        {
            requirementsMet = false;
        }
        
        foreach(QuestInfoSO prerequisiteQuestInfo in quest.questInfoSO.requiredPreviousQuests)
        {
            if(GetQuestById(prerequisiteQuestInfo.questID).questState != QuestState.Completed)
            {
                requirementsMet = false;
            }
        }
        
        return requirementsMet;
    }
    
    private void Update() 
    {
        foreach(Quest quest in questMap.Values)
        {
            if(quest.questState == QuestState.Requirements_Not_Met && CheckRequirementsMet(quest))
            {
                ChangeQuestState(quest.questInfoSO.questID, QuestState.Can_Start);
            }
        }
    }
    
    private void StartQuest(string id)
    {
        // TO-DO: start quest
        Quest quest = GetQuestById(id);
        quest.InstantiateCurrentStep(this.transform);
        ChangeQuestState(quest.questInfoSO.questID, QuestState.In_Progress);
        Debug.Log("Starting quest: " + id);
    }
    
    private void AdvanceQuest(string id)
    {
        // TO-DO: advance quest
        Quest quest = GetQuestById(id);
        quest.MoveToNextStep();
        
        if(quest.CurrentStepExists())
        {
            quest.InstantiateCurrentStep(this.transform);
        } else {
            ChangeQuestState(quest.questInfoSO.questID, QuestState.Can_Complete);
        }
        Debug.Log("Advancing quest: " + id);
    }
    
    private void CompleteQuest(string id)
    {
        Quest quest = GetQuestById(id);
        //ClaimRewards(quest);
        ChangeQuestState(quest.questInfoSO.questID, QuestState.Completed);
        Debug.Log("Completing quest: " + id);
    }
    
    private void ClaimRewards(Quest quest)
    {
        GameEventsManager.instance.playerEvents.TrustGained(quest.questInfoSO.rewardTrustLevel);
    }
    
    private Dictionary<string, Quest> CreateQuestMap()
    {
        QuestInfoSO[] allQuests = Resources.LoadAll<QuestInfoSO>("Quests"); 
        
        Dictionary<string, Quest> idToQuestMap = new Dictionary<string, Quest>();
        
        foreach(QuestInfoSO questInfo in allQuests)
        {
            if(idToQuestMap.ContainsKey(questInfo.questID))
            {
            Debug.LogWarning("There are multiple quests with the same ID: " + questInfo.questID);
        }
            idToQuestMap.Add(questInfo.questID, new Quest(questInfo));
        }
        
        return idToQuestMap;
    }
}
