using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestManager : MonoBehaviour
{
    //[Header("Config")] 
    //[SerializeField] private bool loadQuestState = true;

    public Dictionary<string, Quest> questMap;
    
    public static QuestManager instance;
    
    private int playerTrustAmount;
    
    private void Awake() {
    
        if(instance == null)
        {
            instance = this;
        }else {
            Destroy(gameObject);
        }
    
        questMap = CreateQuestMap();
        
        Quest quest = GetQuestById("FirstImpressionsSO");
        Debug.Log("Quest State: " + quest.questState);
        //Debug.Log(quest.GetCurrentStep());
    }
    
    private void OnEnable() {
        GameEventsManager.instance.questEvents.onStartQuest += StartQuest;
        GameEventsManager.instance.questEvents.onAdvanceQuest += AdvanceQuest;
        GameEventsManager.instance.questEvents.onCompleteQuest += CompleteQuest;
        
        GameEventsManager.instance.questEvents.onQuestStepStateChange += QuestStepStateChange;
        
        GameEventsManager.instance.playerEvents.onTrustGained += TrustGained;
    }
    
    private void OnDisable() {
        GameEventsManager.instance.questEvents.onStartQuest -= StartQuest;
        GameEventsManager.instance.questEvents.onAdvanceQuest -= AdvanceQuest;
        GameEventsManager.instance.questEvents.onCompleteQuest -= CompleteQuest;
        
        GameEventsManager.instance.questEvents.onQuestStepStateChange -= QuestStepStateChange;
        
        GameEventsManager.instance.playerEvents.onTrustGained -= TrustGained;
    }
    
    private void Start()
    {
        foreach(Quest quest in questMap.Values)
        {
            if(quest.questState == QuestState.In_Progress)
            {
                quest.InstantiateCurrentStep(this.transform);
            }
            GameEventsManager.instance.questEvents.QuestStateChange(quest);
        }
    }
    
    public Quest GetQuestById(string id)
    {
        //Debug.Log("GetQuestByID: " + id);
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
    
    private void TrustGained(int trustAmount)
    {
        playerTrustAmount = trustAmount;
    }
    
    private bool CheckRequirementsMet(Quest quest)
    {
        bool requirementsMet = true;
        
        if(playerTrustAmount < quest.questInfoSO.requiredTrustLevel)
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
        //GameEventsManager.instance.questEvents.InvokeStartQuest(id);
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
        ClaimRewards(quest);
        ChangeQuestState(quest.questInfoSO.questID, QuestState.Completed);
        Debug.Log("Completing quest: " + id);
    }
    
    private void ClaimRewards(Quest quest)
    {
        GameEventsManager.instance.playerEvents.TrustGained(quest.questInfoSO.rewardTrustLevel);
    }
    
    private void QuestStepStateChange(string questID, int stepIndex, QuestStepState questStepState)
    {
        Quest quest = GetQuestById(questID);
        quest.StoreQuestStepState(stepIndex, questStepState);
        ChangeQuestState(questID, quest.questState);
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
            idToQuestMap.Add(questInfo.questID, LoadQuest(ref WorldSaveGameManager.instance.currentSaveData, questInfo));
            //Debug.Log("Loaded quest: " + LoadQuest(ref WorldSaveGameManager.instance.currentSaveData, questInfo).questState);
        }
        
        return idToQuestMap;
    }
    
    public void SaveQuest(ref CharacterSaveData saveData)
{
    try
    {
        // Ensure saveData.questDataList is initialized
        if (saveData.questDataList == null)
        {
            saveData.questDataList = new List<CharacterSaveData.QuestDataEntry>();
        }

        // Clear existing quest data
        saveData.questDataList.Clear();

        // Iterate through all quests and save their data
        foreach (Quest quest in questMap.Values)
        {
            QuestData questData = quest.GetQuestData();
            saveData.questDataList.Add(new CharacterSaveData.QuestDataEntry()
            {
                questID = quest.questInfoSO.questID,
                questData = questData
            });
            Debug.Log("Saved quest data: " + questData.state);
            
            if (quest.questState == QuestState.In_Progress || 
                quest.questState == QuestState.Can_Complete || 
                quest.questState == QuestState.Completed)
            {
                saveData.questName = quest.questInfoSO.displayName; // Ensure questName is assigned correctly
            }
        }
        
    } catch (System.Exception e)
    {
        Debug.LogError("Error saving quest data: " + e);
    }
}

    public Quest LoadQuest(ref CharacterSaveData saveData, QuestInfoSO questInfoSO)
{
    Quest quest = null;
    try
    {
        Debug.Log("Loading quest data for quest ID: " + questInfoSO.questID);
        
        if (saveData.questDataList != null)
        {
            foreach (var entry in saveData.questDataList)
            {
                if (entry.questID == questInfoSO.questID)
                {
                    QuestData questData = entry.questData;
                    quest = new Quest(questInfoSO, questData.state, questData.questStepIndex, questData.questStepStates); 
                    Debug.Log("Loaded quest data: " + questInfoSO.questID);
                    break;
                }
            }
        }
        
        if (quest == null)
        {
            Debug.LogWarning("saveData.questDataList is null");
            quest = new Quest(questInfoSO);
        }
    } 
    catch (System.Exception e)
    {
        Debug.LogError("Error loading quest data: " + questInfoSO.questID + ": " + e);
    }
    return quest;
}
}
