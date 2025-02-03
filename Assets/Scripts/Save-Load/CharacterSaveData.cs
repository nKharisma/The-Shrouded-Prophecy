using System.Collections;
using System.Collections.Generic;
using UnityEngine;

    
    [System.Serializable] // This allows the class to be serialized and saved to a file
    //we want to reference this data in every save file so we want this script to be serializable (not monobehavior) so we can save it to a file
    public class CharacterSaveData
    {
        //can only save data with basic data types (int, float, string, etc)
        [Header("Character Info")]
        public string characterName;
        
        [Header("User Stats")]
        public float secondsPlayed;
        
        
        [Header("Player Position")]
        public float xPosition;
        public float yPosition;
        public float zPosition;
        
    }
