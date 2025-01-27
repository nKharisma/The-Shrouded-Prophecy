using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterManager : MonoBehaviour
{

    public CharacterController characterController;
    protected virtual void Awake() { //protected virtual method to be overridden by child classes
        DontDestroyOnLoad(this);
        
        characterController = GetComponent<CharacterController>();
    }
    
    protected virtual void Update()
    {
        
    }
}
