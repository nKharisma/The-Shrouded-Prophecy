using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class QuestStep : MonoBehaviour 
{
    //we make this abstract so that we can have different types of steps
    
    private bool isCompleted = false;
    private string questID;
    
    public void InitializeQuestStep(string questID)
    {
        this.questID = questID;
    }
    
    protected void CompleteStep() {
        if(!isCompleted) {
            isCompleted = true;
            
            GameEventsManager.instance.questEvents.AdvanceQuest(questID);
            
            Destroy(this.gameObject);
        }
    }
}
