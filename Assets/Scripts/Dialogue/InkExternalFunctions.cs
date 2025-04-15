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
        story.BindExternalFunction("LoadSceneInGame", (string sceneIndex, string x, string y, string z) => LoadSceneInGame(sceneIndex, x, y, z));
    }
    
    public void Unbind(Story story)
    {
        story.UnbindExternalFunction("StartQuest");
        story.UnbindExternalFunction("AdvanceQuest");
        story.UnbindExternalFunction("CompleteQuest");
        story.UnbindExternalFunction("LoadSceneInGame");
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
    
    private void LoadSceneInGame(string sceneIndex, string x, string y, string z)
    {
        int index = int.Parse(sceneIndex);
        float xPos = float.Parse(x);
        float yPos = float.Parse(y);
        float zPos = float.Parse(z);
        
        Vector3 position = new Vector3(xPos, yPos, zPos);
        WorldSaveGameManager.instance.StartCoroutine(WorldSaveGameManager.instance.LoadSceneInGame(index, position));
    }
}
