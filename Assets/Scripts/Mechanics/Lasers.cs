using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lasers : MonoBehaviour
{
    private float lifetime;
    private int damage;
    private float currentSpeed = 0;
    private int direction = 1; 

    public bool isPlayerLasers; 

    void Start()
    {
        //asm = GetComponent<AudioSourceManager>(); 
        if (lifetime <= 0) lifetime = 3.0f;
        if (damage <= 0) damage = 1;

        Destroy(gameObject, lifetime);
    }

    private void Update()
    {
        if (!isPlayerLasers) direction = -1; 
        currentSpeed = ObstacleSpawner.Instance.GetCurrentSpeed();
        Vector3 newPosition = transform.position + new Vector3(0, direction, 0) * currentSpeed * Time.deltaTime;
        transform.position = newPosition;
    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        GameObject otherCollider = collision.gameObject;
        if (!isPlayerLasers) 
        {
            if (collision.gameObject.CompareTag("Player") || collision.gameObject.CompareTag("PlayerLasers"))
            {
                if (collision.gameObject.CompareTag("Player"))
                {
                    GameManager.Instance.Lives--;
                }
                Destroy(gameObject);
            }

        }
        else if (isPlayerLasers)
        {

            if (collision.gameObject.layer == 6)
            {
                collision.gameObject.GetComponent<EnemyObstacles>().TakeDamage(damage);
                Debug.Log("EnemyHit!"); 
                Destroy(gameObject);
            }
            else if (gameObject.CompareTag("EnemyLasers"))
            {
                Destroy(gameObject);
            }
        }
    }

}
