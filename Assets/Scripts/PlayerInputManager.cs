using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInputManager : MonoBehaviour
{
    Input inputActions;
    [SerializeField] Vector2 movement; //serialized for debugging purposes
    
    private void OnEnable() {
        if (inputActions == null) {
            inputActions = new Input();
            
            inputActions.PlayerMovement.Movement.performed += i => movement = i.ReadValue<Vector2>();
        }
        inputActions.Enable();    
    }
}
