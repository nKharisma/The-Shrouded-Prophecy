using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Quest
{
    public QuestInfoSO questInfoSO;
    public QuestState questState;
    
    public int currentQuestStepIndex { get; private set; }
    
    public QuestStepState[] questStepStates;
    
    public Quest(QuestInfoSO questInfoSO)
    {
        this.questInfoSO = questInfoSO;
        this.questState = QuestState.Requirements_Not_Met;
        this.currentQuestStepIndex = 0;
        this.questStepStates = new QuestStepState[questInfoSO.questStepsPrefabs.Count];
        for(int i = 0; i < questStepStates.Length; i++)
        {
            questStepStates[i] = new QuestStepState();
        }
    }
    
    public Quest(QuestInfoSO questInfo, QuestState questState, int currentQuestStepIndex, QuestStepState[] questStepStates)
    {
        this.questInfoSO = questInfo;
        this.questState = questState;
        this.currentQuestStepIndex = currentQuestStepIndex;
        this.questStepStates = questStepStates;

        // if the quest step states and prefabs are different lengths,
        // something has changed during development and the saved data is out of sync.
        if (this.questStepStates.Length != this.questInfoSO.questStepsPrefabs.Count)
        {
            Debug.LogWarning("Quest Step Prefabs and Quest Step States are "
                + "of different lengths. This indicates something changed "
                + "with the QuestInfo and the saved data is now out of sync. "
                + "Reset your data - as this might cause issues. QuestId: " + this.questInfoSO.questID);
        }
    }
    
    public void MoveToNextStep()
    {
        if(currentQuestStepIndex < questInfoSO.questStepsPrefabs.Count && questInfoSO != null)
        {
            currentQuestStepIndex++;
        }
    }
    
    public bool CurrentStepExists()
    {
        return currentQuestStepIndex < questInfoSO.questStepsPrefabs.Count;
    }
    
    public void InstantiateCurrentStep(Transform parentTransform)
    {
        GameObject currentStep = GetCurrentStep();
        if(currentStep != null)
        {
            QuestStep questStep = Object.Instantiate<GameObject>(currentStep, parentTransform)
                .GetComponent<QuestStep>();
            questStep.InitializeQuestStep(questInfoSO.questID, currentQuestStepIndex, questStepStates[currentQuestStepIndex].state);
        }
    }
    
    public GameObject GetCurrentStep()
    {
        GameObject questStepPrefab = null;
        
        if(CurrentStepExists())
        {
            questStepPrefab = questInfoSO.questStepsPrefabs[currentQuestStepIndex];
        }else {
            Debug.LogWarning("Tried to get next step but there are no more steps meaning that currentStepIndex is out of range " + "there is no current step QuestID: " + questInfoSO.questID + "currentStepIndex: " + currentQuestStepIndex);
        }
        
        return questStepPrefab;
    }
    
    public void StoreQuestStepState(int stepIndex, QuestStepState questStepState)
    {
        if(stepIndex < questStepStates.Length)
        {
            questStepStates[stepIndex] = questStepState;
        }else {
            Debug.LogWarning("Tried to store quest step state but stepIndex is out of range " + "QuestID: " + questInfoSO.questID + "stepIndex: " + stepIndex);
        }
    }
    
    public QuestData GetQuestData()
    {
        return new QuestData(questState, currentQuestStepIndex, questStepStates);
    }
    
    public string GetFullStatusText()
    {
        string fullStatus = "";

        if (questState == QuestState.Requirements_Not_Met)
        {
            fullStatus = "Requirements are not yet met to start this quest.";
        }
        else if (questState == QuestState.Can_Start)
        {
            fullStatus = "This quest can be started!";
        }
        else 
        {
            // display all previous quests with strikethroughs
            for (int i = 0; i < currentQuestStepIndex; i++)
            {
                fullStatus += "<s>" + questStepStates[i].status + "</s>\n";
            }
            // display the current step, if it exists
            if (CurrentStepExists())
            {
                fullStatus += questStepStates[currentQuestStepIndex].status;
            }
            // when the quest is completed or turned in
            if (questState == QuestState.Can_Complete)
            {
                fullStatus += "The quest is ready to be turned in.";
            }
            else if (questState == QuestState.Completed)
            {
                fullStatus += "The quest has been completed!";
            }
        }
        return fullStatus;
    }
}
