using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Ink.Runtime;

public class InkExternalFunctions
{

    public void Bind(Story story)
    {
        story.BindExternalFunction("StartQuest", (string questID) => StartQuest(questID));
        story.BindExternalFunction("AdvanceQuest", (string questID) => AdvanceQuest(questID));
        story.BindExternalFunction("CompleteQuest", (string questID) => CompleteQuest(questID));
    }
    
    public void Unbind(Story story)
    {
        story.UnbindExternalFunction("StartQuest");
        story.UnbindExternalFunction("AdvanceQuest");
        story.UnbindExternalFunction("CompleteQuest");
    }

    private void StartQuest(string questID)
    {
        GameEventsManager.instance.questEvents.StartQuest(questID);
    }
    
    private void AdvanceQuest(string questID)
    {
        GameEventsManager.instance.questEvents.AdvanceQuest(questID);
    }
    
    private void CompleteQuest(string questID)
    {
        GameEventsManager.instance.questEvents.CompleteQuest(questID);
    }
}
