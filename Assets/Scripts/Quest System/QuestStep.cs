using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class QuestStep : MonoBehaviour 
{
    //we make this abstract so that we can have different types of steps
    
    private bool isCompleted = false;
    public string questID { get; private set; }
    public int currentStepIndex { get; private set;}
    
    public void InitializeQuestStep(string questID, int stepIndex, string questStepState)
    {
        this.questID = questID;
        this.currentStepIndex = stepIndex;
        if(questStepState != null && questStepState != "")
        {
            SetQuestStepState(questStepState);
        }
    }
    
    protected void CompleteStep() {
        if(!isCompleted) {
            isCompleted = true;
            
            GameEventsManager.instance.questEvents.AdvanceQuest(questID);
            
            Destroy(this.gameObject);
        }
    }
    
    protected void ChangeState(string newState, string newStatus)
    {
        GameEventsManager.instance.questEvents.QuestStepStateChange(questID, currentStepIndex, new QuestStepState(newState, newStatus));
    }
    
    protected abstract void SetQuestStepState(string questStepState);
}
