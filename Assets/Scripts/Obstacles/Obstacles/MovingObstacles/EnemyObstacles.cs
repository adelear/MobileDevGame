using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyObstacles : MovingObjects
{
    protected void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            if (gameObject.CompareTag("Start"))
            {
                ObstacleSpawner.Instance.SpawnObstacle(2);
            }

            Debug.Log("Player hit!");
            if (!other.gameObject.GetComponent<PlayerAbilities>().invincibilityActive) GameManager.Instance.Lives--;
            Destroy(gameObject);
        }
    }
}
