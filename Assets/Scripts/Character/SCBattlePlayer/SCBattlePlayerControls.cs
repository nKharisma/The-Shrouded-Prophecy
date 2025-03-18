using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SCBattlePlayerControls : MonoBehaviour
{
    [SerializeField] public float movSpeed;
    [SerializeField] private RectTransform canvas;
    [SerializeField] private RectTransform skillCheckSquare;
    
    private Vector2 moveInput;
    private Vector2 moveVelocity;

    void Update()
    {
        moveInput.x = Input.GetAxisRaw("Horizontal") * movSpeed;
        moveInput.y = Input.GetAxisRaw("Vertical") * movSpeed;

        moveVelocity = moveInput.normalized * movSpeed;

        skillCheckSquare.anchoredPosition += moveVelocity * Time.deltaTime;

        ClampPanel();
    }

    private void ClampPanel()
    {
        Vector3[] canvasCorners = new Vector3[4];
        canvas.GetWorldCorners(canvasCorners);

        Vector3[] squareCorners = new Vector3[4];
        skillCheckSquare.GetWorldCorners(squareCorners);

        float minX = canvasCorners[0].x - squareCorners[0].x + skillCheckSquare.anchoredPosition.x;
        float maxX = canvasCorners[2].x - squareCorners[2].x + skillCheckSquare.anchoredPosition.x;
        float minY = canvasCorners[0].y - squareCorners[0].y + skillCheckSquare.anchoredPosition.y;
        float maxY = canvasCorners[2].y - squareCorners[2].y + skillCheckSquare.anchoredPosition.y;

        float clampedX = Mathf.Clamp(skillCheckSquare.anchoredPosition.x, minX, maxX);
        float clampedY = Mathf.Clamp(skillCheckSquare.anchoredPosition.y, minY, maxY);

        skillCheckSquare.anchoredPosition = new Vector2(clampedX, clampedY);
    }
}





