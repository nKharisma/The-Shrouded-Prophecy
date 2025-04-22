using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class GameEventsManager : MonoBehaviour
{
    public static GameEventsManager instance { get; private set; }
    public QuestEvents questEvents;
    public DialogueEvents dialogueEvents;
    public PlayerEvents playerEvents;
    
    private void Awake() {
        if(instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }else {
            Destroy(this);
        }
        
        questEvents = new QuestEvents();
        dialogueEvents = new DialogueEvents();
        playerEvents = new PlayerEvents();
    }
}
