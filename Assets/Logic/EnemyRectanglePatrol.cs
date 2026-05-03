using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyRectanglePatrol : MonoBehaviour
{
    public Transform[] patrolPoints;

    public float patrolSpeed = 2f;
    public float chaseSpeed = 4f;

    Transform player;

    int currentPoint = 0;

    bool isChasing = false;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    void Update()
    {
        if (isChasing)
            ChasePlayer();
        else
            PatrolRectangle();
    }

    void PatrolRectangle()
    {
        Transform target = patrolPoints[currentPoint];

        transform.position = Vector2.MoveTowards(
            transform.position,
            target.position,
            patrolSpeed * Time.deltaTime
        );

        if (Vector2.Distance(transform.position, target.position) < 0.1f)
        {
            currentPoint++;

            if (currentPoint >= patrolPoints.Length)
                currentPoint = 0;
        }
    }

    void ChasePlayer()
    {
        transform.position = Vector2.MoveTowards(
            transform.position,
            player.position,
            chaseSpeed * Time.deltaTime
        );
    }

    public void StartChase()
    {
        isChasing = true;
    }

    public void StopChase()
    {
        isChasing = false;
    }
}