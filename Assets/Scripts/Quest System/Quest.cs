using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Quest
{
    public QuestInfoSO questInfoSO;
    public QuestState questState;
    
    private int currentStepIndex;
    
    //private QuestStepState[] questStepStates;
    
    public Quest(QuestInfoSO questInfoSO)
    {
        this.questInfoSO = questInfoSO;
        this.questState = QuestState.Requirements_Not_Met;
        this.currentStepIndex = 0;
    }
    
    public void MoveToNextStep()
    {
        if(currentStepIndex < questInfoSO.questStepsPrefabs.Count && questInfoSO != null)
        {
            currentStepIndex++;
        }
    }
    
    public bool CurrentStepExists()
    {
        return currentStepIndex < questInfoSO.questStepsPrefabs.Count;
    }
    
    public void InstantiateCurrentStep(Transform parentTransform)
    {
        GameObject currentStep = GetCurrentStep();
        if(currentStep != null)
        {
            QuestStep questStep = Object.Instantiate<GameObject>(currentStep, parentTransform)
                .GetComponent<QuestStep>();
            questStep.InitializeQuestStep(questInfoSO.questID);
        }
    }
    
    public GameObject GetCurrentStep()
    {
        GameObject questStepPrefab = null;
        
        if(CurrentStepExists())
        {
            questStepPrefab = questInfoSO.questStepsPrefabs[currentStepIndex];
        }else {
            Debug.LogWarning("Tried to get next step but there are no more steps meaning that currentStepIndex is out of range " + "there is no current step QuestID: " + questInfoSO.questID + "currentStepIndex: " + currentStepIndex);
        }
        
        return questStepPrefab;
    }
}
