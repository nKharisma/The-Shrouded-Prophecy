using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SCBattlePlayerControls : MonoBehaviour
{
    [SerializeField] private float movSpeed;
    [SerializeField] private RectTransform skillCheckSquare;  // Reference to the square in the canvas
    
    private Vector2 moveInput;
    private Vector2 moveVelocity;

    void Update()
    {
        // Get input for movement (use any method you prefer, such as keyboard input)
        moveInput.x = Input.GetAxisRaw("Horizontal") * movSpeed;
        moveInput.y = Input.GetAxisRaw("Vertical") * movSpeed;

        // Move the square based on the input
        moveVelocity = moveInput.normalized * movSpeed;

        // Update position based on RectTransform (UI element movement)
        skillCheckSquare.anchoredPosition += moveVelocity * Time.deltaTime;
    }
}





