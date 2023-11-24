using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingObjects : Obstacles
{
    [SerializeField] private Transform obstacleStart;
    [SerializeField] private Transform obstacleEnd;
    private int health = 4; 

    protected float maxSpeed = 10.0f;
    protected float initialSpeed = 2.0f;
    protected float speedIncreaseRate = 0.0267f;
    private float currentSpeed;

    public event Action<GameObject> OnObstacleDestroyed;
    //vf = vi + a*t
    // t = (vf - vi)/a 
    // If acceleration is 0.01, it takes 13 minutes to get to max speed 


    protected virtual void Start()
    {
        currentSpeed = initialSpeed; 
        
    }
    public void TakeDamage(int damage)
    {
        health -= damage;
    }

    protected virtual void Update()
    {
        currentSpeed = ObstacleSpawner.Instance.GetCurrentSpeed(); 

        Vector3 newPosition = transform.position + new Vector3(0, -1, 0) * currentSpeed * Time.deltaTime; 
        transform.position = newPosition;
        if (gameObject.CompareTag("Start"))
        {
            if (transform.position.y <= obstacleEnd.position.y) ObstacleSpawner.Instance.SpawnObstacle(1);
        }

        if (transform.position.y <= obstacleEnd.position.y)
        {
            OnObstacleDestroyed?.Invoke(gameObject);
            Destroy(gameObject); 
        }

        if (health <= 0)
        {
            OnObstacleDestroyed?.Invoke(gameObject); 
            Destroy(gameObject);
        }
    }
}
