using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;

    
    [System.Serializable] // This allows the class to be serialized and saved to a file
    //we want to reference this data in every save file so we want this script to be serializable (not monobehavior) so we can save it to a file
    public class CharacterSaveData
    {
        //can only save data with basic data types (int, float, string, etc)
        [Header("Character Info")]
        public string questName;
        public List<QuestDataEntry> questDataList;
        
        [Header("User Stats")]
        public float secondsPlayed;
        
        [Header("Player Position")]
        public float xPosition;
        public float yPosition;
        public float zPosition;
        
                public CharacterSaveData()
            {
                questDataList = new List<QuestDataEntry>();
            }
        
            [Serializable]
            public class QuestDataEntry
            {
                public string questID;
                public QuestData questData;
            }
    }
