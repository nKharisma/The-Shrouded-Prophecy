using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : CharacterMovement
{

    PlayerManager player;
    public float verticalMovement;
    public float horizontalMovement;
    public float moveAmount;
    
    private Vector3 moveDirection;
    [SerializeField] float walkSpeed = 2;
    [SerializeField] float runSpeed = 4.5f;

    protected override void Awake()
    {
        base.Awake();
        
        player = GetComponent<PlayerManager>();
    }

    public void Movement()
    {
        GroundMovement();
    }
    
    private void GetVerticalAndHorizontalMovement()
    {
        verticalMovement = PlayerInputManager.instance.vertical;
        horizontalMovement = PlayerInputManager.instance.horizontal;
    }
    
    private void GroundMovement()
    {
    /*
        verticalMovement = player.inputManager.vertical;
        horizontalMovement = player.inputManager.horizontal;
        
        moveAmount = Mathf.Clamp01(Mathf.Abs(horizontalMovement) + Mathf.Abs(verticalMovement));
        
        moveDirection = player.cameraManager.transform.forward * verticalMovement;
        moveDirection += player.cameraManager.transform.right * horizontalMovement;
        
        moveDirection.Normalize();
        moveDirection.y = 0;
        
        float targetSpeed = player.moveSpeed;
        if (player.isSprinting)
        {
            targetSpeed = player.sprintSpeed;
        }
        
        Vector3 targetDirection = moveDirection;
        targetDirection *= targetSpeed;
        targetDirection.y = player.rigidbody.velocity.y;
        
        player.rigidbody.velocity = targetDirection;
        */
        
        GetVerticalAndHorizontalMovement();
        
        moveDirection = PlayerCamera.instance.transform.forward * verticalMovement; //set the move direction to the forward vector of the camera multiplied by the vertical input
        moveDirection += PlayerCamera.instance.transform.right * horizontalMovement; //add the right vector of the camera multiplied by the horizontal input to the move direction
        moveDirection.Normalize();
        moveDirection.y = 0; //set the y value of the move direction to 0
        
        if(PlayerInputManager.instance.moveAmount > 0.5f)
        {
            player.characterController.Move(moveDirection * runSpeed * Time.deltaTime);
        }else if(PlayerInputManager.instance.moveAmount < 0.5f)
        {
            player.characterController.Move(moveDirection * walkSpeed * Time.deltaTime);
        }
    }
}
