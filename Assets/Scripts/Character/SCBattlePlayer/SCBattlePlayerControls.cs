using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SCBattlePlayerControls : MonoBehaviour
{
    [SerializeField] public float movSpeed;
    float speedX, speedY;
    Rigidbody2D rigidBod;

    // Start is called before the first frame update
    void Start()
    {
      rigidBod = GetComponent<Rigidbody2D>();  
    }

    // Update is called once per frame
    void Update()
    {
       speedX = Input.GetAxisRaw("Horizontal") * movSpeed;
       speedY = Input.GetAxisRaw("Vertical") * movSpeed;
       rigidBod.velocity = new Vector2(speedX, speedY);
    }
}
