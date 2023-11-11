using UnityEngine;
using UnityEngine.SceneManagement;
using static GameManager;

public class LevelManager : MonoBehaviour
{
    public static LevelManager Instance { get; private set; }
    [SerializeField] private GameObject spawnPoint;
    [SerializeField] private GameObject enemyPrefab;
    [SerializeField] private int currentLevel = 1;  
    private float currentTime = 0;
    [SerializeField] private int currentWaveEnemyCount = 0;
    [SerializeField] private float timeBetweenEnemies = 5.0f; 

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
        GameManager.Instance.SwitchState(GameState.GAME);
        StartNewLevel();
    }

    private void Update()
    {
        if (GameManager.Instance.GetGameState() != GameState.GAME) return;

        if (PlayerWinsLevel())
        {
            StartNewLevel();
        }
        else
        {
            currentTime += Time.deltaTime;
            SpawnEnemies();
        } 
    }

    private void StartNewLevel()
    {
        if (currentLevel == 1)
        {
            currentWaveEnemyCount = 5; // Setting initial number of enemies for the first level.
        }
        else
        {
            int previousEnemyCount = currentWaveEnemyCount; // Storing previous enemy count.

            // Calculate the new enemy count based on the previous count and current level.
            currentWaveEnemyCount = previousEnemyCount + (currentLevel * 5);
        }

        timeBetweenEnemies = 5.0f; // Reseting time between enemies for each level.
        currentLevel++;
    }


    private bool PlayerWinsLevel()
    {
        // Checking if all enemies have been destroyed
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy"); 
        // Checking if all enemies have been spawned
        bool allEnemiesSpawned = currentWaveEnemyCount == 0;
        // If there are no enemies in the scene and all have been spawned, the player moves on to next round
        return allEnemiesSpawned && enemies.Length == 0;
    }

    private void SpawnEnemies()
    {
        if (currentWaveEnemyCount > 0 && currentTime >= timeBetweenEnemies)
        {
            float minSpeed, maxSpeed;

            if (currentLevel % 5 == 0)
            {
                // It's a "hard" level, set the speed range to 0.5 - 2
                minSpeed = 0.5f;
                maxSpeed = 2f;
            }
            else
            {
                // It's an "easy" level, set the speed range to 1 - 1
                minSpeed = 2f;
                maxSpeed = 4f;
            }

            timeBetweenEnemies = Random.Range(minSpeed, maxSpeed); 

            Instantiate(enemyPrefab, spawnPoint.transform.position, transform.rotation);
            Debug.Log("Enemies Spawned");
            currentWaveEnemyCount--;
            currentTime = 0;
        }
    }

}
