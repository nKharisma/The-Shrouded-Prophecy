using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class World_Patrol : MonoBehaviour
{
    public float speed;
    private float waitTime;
    public float startWaitTime;

    public Transform[] waypoints;
    private int randomWaypoint;
    
    private Animator animator;

    void Start()
    {
        waitTime = startWaitTime;
        randomWaypoint = Random.Range(0, waypoints.Length);
        
        animator = GetComponent<Animator>();
        if (animator == null)
        {
            Debug.LogError("Animator component not found on the GameObject.");
        }
    }

    void Update()
    {
        // Move towards the target waypoint in 3D space
        transform.position = Vector3.MoveTowards(transform.position, waypoints[randomWaypoint].position, speed * Time.deltaTime);
        
        Vector3 targetDirection = waypoints[randomWaypoint].position - transform.position;
        
        float direction = targetDirection.x < 0 ? -1 : 1;

        // Check if the GameObject is close to the waypoint
        if (Vector3.Distance(transform.position, waypoints[randomWaypoint].position) < 0.2f)
        {
            if (waitTime <= 0)
            {
                // Select a new random waypoint
                randomWaypoint = Random.Range(0, waypoints.Length);
                waitTime = startWaitTime;
                
                if(animator != null)
                {
                    animator.SetBool("isWalking", false);
                }
            }
            else
            {
                // Wait at the current waypoint
                waitTime -= Time.deltaTime;
                
                if(animator != null)
                {
                    animator.SetBool("isWalking", false);
                }
            }
        }else {
            if(animator != null)
            {
                animator.SetBool("isWalking", true);
                animator.SetFloat("WalkDirection", direction);
            }
        }
    }
}
