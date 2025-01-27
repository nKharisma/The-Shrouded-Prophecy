using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerInputManager : MonoBehaviour
{
    public static PlayerInputManager instance; //singleton instance
    PlayerControls inputActions; //input actions reference
    [SerializeField] Vector2 movement; //serialized for debugging purposes
    [SerializeField] public float horizontal; //serialized for debugging purposes
    [SerializeField] public float vertical; //serialized for debugging purposes
    public float moveAmount; //serialized for debugging purposes
    
    
    private void Awake() {
        if(instance == null) {
            instance = this; //set the singleton instance
        } else {
            Destroy(gameObject); //destroy the game object if there is already an instance
        }
    }
    
    private void Start() {
        DontDestroyOnLoad(gameObject); //don't destroy the game object when loading a new scene
        
        SceneManager.activeSceneChanged += OnSceneChange; //subscribe to the active scene changed event
        
        instance.enabled = false;
    }
    
    private void Update() {
        MovementInput(); //call the movement input method
    }
    
    private void OnSceneChange(Scene current, Scene next) {
        if (next.buildIndex == WorldSaveGameManager.instance.GetWorldSceneIndex()) {
            instance.enabled = true;
        }else {
            instance.enabled = false;
        }
    }
    private void OnEnable() {
        if (inputActions == null) {
            inputActions = new PlayerControls();
            
            inputActions.PlayerMovement.Movement.performed += i => movement = i.ReadValue<Vector2>(); //when movement is performed, the movement vector is updated by the input value
        }
        inputActions.Enable(); //enable input actions
    }
    
    private void OnDestroy() {
        SceneManager.activeSceneChanged -= OnSceneChange; //unsubscribe from the active scene changed event
    }
    
    private void MovementInput() {
        vertical = movement.y; //set the vertical input to the y value of the movement vector
        horizontal = movement.x; //set the horizontal input to the x value of the movement vector
        
        moveAmount = Mathf.Clamp01(Mathf.Abs(horizontal) + Mathf.Abs(vertical)); //clamp the move amount between 0 and 1 in order to prevent the player from moving faster diagonally
        
        if(moveAmount <= 0.5 && moveAmount > 0)
        {
            moveAmount = 0.5f; 
        }else if (moveAmount > 0.5 && moveAmount <= 1)
        {
            moveAmount = 1f;
        }
    }
}
