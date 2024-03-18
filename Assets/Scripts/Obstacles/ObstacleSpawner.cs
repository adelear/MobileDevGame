using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static GameManager;

public class ObstacleSpawner : MonoBehaviour
{ 
    public static ObstacleSpawner Instance { get; private set; } 
    [SerializeField] private List<GameObject> obstaclePrefabs; 
    [SerializeField] private Transform startPoint1;
    [SerializeField] private Transform startPoint2;
    [SerializeField] private float maxSpeed = 10.0f; 
    [SerializeField] private float initialSpeed = 2.0f;
    private float speedIncreaseRate = 0.0267f;
    private float currentSpeed; // Store the current speed here 

    private bool canSpawn = false; 

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }
    }

    private void Start()
    {
        currentSpeed = initialSpeed; 
        SpawnObstacle(1); 
    }

    public void SpawnObstacle(int startingPoint)
    {
        int randomIndex = Random.Range(0, obstaclePrefabs.Count);
        GameObject obstaclePrefab = obstaclePrefabs[randomIndex];
        if (startingPoint == 1) Instantiate(obstaclePrefab, startPoint1.position, startPoint1.rotation);
        else Instantiate(obstaclePrefab, startPoint2.position, startPoint2.rotation);
    }

    public bool GetCanSpawn()
    {
        return canSpawn; 
    }
     
    public void SetCanSpawn(bool flag) 
    { 
        canSpawn = flag; 
    }

    private void UpdateSpeed()
    {
        currentSpeed += speedIncreaseRate * Time.deltaTime;
        if (currentSpeed > maxSpeed) currentSpeed = maxSpeed; 
    }

    public float GetCurrentSpeed()
    {
        return currentSpeed; 
    }

    private void Update()
    {
        UpdateSpeed();
    }

}
