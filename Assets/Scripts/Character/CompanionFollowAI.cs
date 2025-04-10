using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CompanionFollowAI : MonoBehaviour
{
    public Transform player;

    public float speed = 5f;
    public float distanceFromPlayer = 2f;

    private Rigidbody2D rigidBody;

    // Start is called before the first frame update
    void Start()
    {
        rigidBody = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
       Vector3 direction = (player.position - transform.position);
       float distance = direction.magnitude;

       if (distance > distanceFromPlayer)
       {
            direction.Normalize();
            Vector3 move = direction * speed * Time.fixedDeltaTime;

            transform.position += move;
       }
    }
}
