using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingObstacles : Obstacles
{
    [SerializeField] private Transform obstacleStart;
    [SerializeField] private Transform obstacleEnd;
    protected float maxSpeed = 10.0f;
    protected float initialSpeed = 2.0f;
    protected float speedIncreaseRate = 0.0267f;
    private float currentSpeed; 
    //vf = vi + a*t
    // t = (vf - vi)/a 
    // If acceleration is 0.01, it takes 13 minutes to get to max speed 


    protected virtual void Start()
    {
        currentSpeed = initialSpeed; 
    }

    protected virtual void Update()
    {
        currentSpeed += speedIncreaseRate * Time.deltaTime; 
        if (currentSpeed > maxSpeed) currentSpeed = maxSpeed; 
        Vector3 newPosition = transform.position + new Vector3(0, -1, 0) * currentSpeed * Time.deltaTime; 
        transform.position = newPosition;
        if (transform.position.y <= obstacleEnd.position.y)
        {
            newPosition.y = obstacleStart.transform.position.y;
            transform.position = newPosition; 
        }
    }

    protected void OnCollisionEnter2D(Collision2D other)
    { 
        if (other.gameObject.CompareTag("Player"))
        {
            Debug.Log("Player hit!");
            Destroy(gameObject);
        }
    }

}
