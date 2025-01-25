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
}
