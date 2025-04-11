using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "QuestInfoSO", menuName = "Quest System/QuestInfoSO", order = 1)] // Create a new asset in the project window

public class QuestInfoSO : ScriptableObject 
{
    [field: SerializeField] public string questID { get; private set; } // Unique ID for the quest
    
    //the id is always the name of the Scriptable Object asset (as long as scriptable object name is unique )
    
    [Header("Quest Info")]
    public string displayName;
    
    [Header("Requirements")]
    public int requiredTrustLevel;
    
    public List<QuestInfoSO> requiredPreviousQuests;
    
    [Header("Steps")]
    public List<GameObject> questStepsPrefabs;
    
    [Header("Rewards")]
    public int rewardTrustLevel;
    
    private void OnValidate() {
        #if UNITY_EDITOR 
        questID = this.name; // Set the ID to the name of the asset
        UnityEditor.EditorUtility.SetDirty(this); // Mark the asset as dirty so it gets saved
        #endif
    }
}
