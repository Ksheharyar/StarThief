using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RectangleDetection : MonoBehaviour
{
    public EnemyRectanglePatrol enemy;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            enemy.StartChase();
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            enemy.StopChase();
        }
    }
}