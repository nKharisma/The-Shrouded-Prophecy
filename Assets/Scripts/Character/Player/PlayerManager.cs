using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerManager : CharacterManager //inherit from CharacterManager
{
    public PlayerMovement playerMotionManager;
    public PlayerAnimatorManager playerAnimatorManager;
    
    protected override void Awake() { 
        base.Awake();
        
        //stuff only for the player
        playerMotionManager = GetComponent<PlayerMovement>();
        playerAnimatorManager = GetComponent<PlayerAnimatorManager>();
        PlayerInputManager.instance.player = this;
        WorldSaveGameManager.instance.player = this;
    }
    
    protected override void Update()
    {
        if (DialogueManager.GetInstance().dialogueIsPlaying)
        {
            return;
        }

        base.Update();
        
        //stuff only for the player
        playerMotionManager.Movement();
    }
    
    public void SavePlayerData(ref CharacterSaveData saveData)
    {
        saveData.sceneIndex = SceneManager.GetActiveScene().buildIndex;
        saveData.yPosition = transform.position.y;
        saveData.xPosition = transform.position.x;
        saveData.zPosition = transform.position.z;
    }
    
    public void LoadPlayerData(ref CharacterSaveData saveData)
    {
        transform.position = new Vector3(saveData.xPosition, saveData.yPosition, saveData.zPosition);
    }
}
