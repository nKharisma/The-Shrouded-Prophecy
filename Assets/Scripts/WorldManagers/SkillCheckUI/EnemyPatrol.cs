using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPatrol : MonoBehaviour
{
    [SerializeField] public Transform[] patrolPoints;
    [SerializeField] public int targetPoint;
    [SerializeField] public float speed;

    void Start()
    {
        targetPoint = 0;
    }

    void Update()
    {
        if (transform.position == patrolPoints[targetPoint].position)
        {
            nextTarget();
        }

        transform.position = Vector2.MoveTowards(transform.position, patrolPoints[targetPoint].position, speed * Time.deltaTime);
    }

    void OnDisable()
    {
        targetPoint = 0;
        transform.position = patrolPoints[0].position;
    }

    private void nextTarget()
    {
        targetPoint++;

        if (targetPoint >= patrolPoints.Length)
        {
            targetPoint = 0;
        }
    }
}
