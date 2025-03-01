using System;

public class QuestEvents
{
    public event Action<string> onStartQuest;
    public void StartQuest(string questID)
    {
        if(onStartQuest != null)
        {
            onStartQuest(questID);
        }
    }
    
    public event Action<string> onAdvanceQuest;
    public void AdvanceQuest(string questID)
    {
        if(onAdvanceQuest != null)
        {
            onAdvanceQuest(questID);
        }
    }
    
    public event Action<string> onCompleteQuest;
    public void CompleteQuest(string questID)
    {
        if(onCompleteQuest != null)
        {
            onCompleteQuest(questID);
        }
    }
    
    public event Action<Quest> onQuestStateChange;
    public void QuestStateChange(Quest quest)
    {
        if(onQuestStateChange != null)
        {
            onQuestStateChange(quest);
        }
    }
    /*
    public event Action<string, int, QuestStepState> onQuestStepStateChange;
    public void QuestStepChange(string questID, int stepIndex, QuestStepState questStepState)
    {
        if(onQuestStepChange != null)
        {
            onQuestStepChange(id, stepIndex, questStepState);
        }
    }*/
}
