using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : CharacterManager //inherit from CharacterManager
{
    PlayerMovement playerMotionManager;
    protected override void Awake() { 
        base.Awake();
        
        //stuff only for the player
        playerMotionManager = GetComponent<PlayerMovement>();
    }
    
    protected override void Update()
    {
        base.Update();
        
        //stuff only for the player
        playerMotionManager.Movement();
    }
    
    public void SavePlayerData(ref CharacterSaveData saveData)
    {
        //saveData.characterName = characterName; //haven't defined characterName yet
        saveData.yPosition = transform.position.y;
        saveData.xPosition = transform.position.x;
        saveData.zPosition = transform.position.z;
    }
    
    public void LoadPlayerData(ref CharacterSaveData saveData)
    {
        transform.position = new Vector3(saveData.xPosition, saveData.yPosition, saveData.zPosition);
        //characterName stuff here
    }
}
