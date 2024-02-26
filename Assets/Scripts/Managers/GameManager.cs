using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEditor.U2D.Aseprite;

public class GameManager : MonoBehaviour
{
    [SerializeField] GameObject HealthImg;
    [SerializeField] AudioManager asm;
    [SerializeField] AudioClip LossSound;

    [SerializeField] List<CatData> catData = new List<CatData>(); 
    [SerializeField] List<CatData> ownedCats = new List<CatData>();
    public List<CatData> OwnedCats => ownedCats;

    private int scoreMultiplier;
    public static GameManager Instance { get; private set; }
    public enum GameState
    {
        GAME,
        PAUSE,
        DEFEAT,
        WIN
    }
    [SerializeField] private GameState currentState;

    public event Action OnGameStateChanged;

    public void Awake()
    {
        if (Instance == null)
            Instance = this;

        if (Instance != this)
            Destroy(gameObject);

        LoadHighScore();
        LoadCoins();
        LoadOwnedCats();
        DontDestroyOnLoad(gameObject); 
    }

    public int Score
    {
        get => score;
        set
        {
            score = value;
            if (score > highScore)
            {
                highScore = score;
                SaveHighScore();
                OnHighScoreChanged?.Invoke(highScore);
            }
            if (OnScoreValueChanged != null)
                OnScoreValueChanged.Invoke(score);
        }
    }
    private int score = 0;
    public UnityEvent<int> OnScoreValueChanged;

    public int Coins
    {
        get => coins;
        set
        {
            coins = value;
            SaveCoins();
            if (OnCoinsValueChanged != null)
                OnCoinsValueChanged.Invoke(coins);
        }
    }
    private int coins = 0;
    public UnityEvent<int> OnCoinsValueChanged;

    public int CurrentCoins
    {
        get => currentCoins;
        set
        {
            currentCoins = value;
            if (OnCurrentCoinsValueChanged != null)
                OnCurrentCoinsValueChanged.Invoke(currentCoins);
        }
    }
    private int currentCoins = 0;
    public UnityEvent<int> OnCurrentCoinsValueChanged;

    public int Lives
    {
        get => lives;
        set
        {
            if (value > lives) IncreaseHealthBar();
            else if (value < lives) DecreaseHealthBar();
            lives = value;

            if (lives > maxLives) lives = maxLives;

            Debug.Log("Lives value has changed to " + lives.ToString());
            if (lives < 0)
                StartCoroutine(DelayedGameOver(0.5f));

            if (OnLifeValueChanged != null)
                OnLifeValueChanged.Invoke(lives);
        }
    }

    public UnityEvent<int> OnLifeValueChanged;

    private int lives = 3;
    public int maxLives = 3;

    public int HighScore
    {
        get => highScore;
        set
        {
            highScore = value;
            if (OnHighScoreChanged != null)
                OnHighScoreChanged.Invoke(highScore);
        }
    }
    private int highScore = 0;
    public UnityEvent<int> OnHighScoreChanged;

    public void DecreaseHealthBar()
    {
        if (HealthImg != null)
        {
            RectTransform healthRectTransform = HealthImg.GetComponent<RectTransform>();
            healthRectTransform.sizeDelta -= new Vector2(10f, 0f);
        }
    }
    public void IncreaseHealthBar()
    {
        if (HealthImg != null)
        {
            RectTransform healthRectTransform = HealthImg.GetComponent<RectTransform>();
            healthRectTransform.sizeDelta += new Vector2(10f, 0f);
        }
    }

    IEnumerator DelayedGameOver(float delay)
    {
        yield return new WaitForSeconds(delay);
        GameOver();
    }

    void GameOver()
    {
        SwitchState(GameState.DEFEAT);
        SceneManager.LoadScene("MainMenu");
        asm.PlayOneShot(LossSound, false);
        Lives = 3;
    }

    void SaveCoins()
    {
        PlayerPrefs.SetInt("Coins", coins);
        PlayerPrefs.Save();
    }

    void LoadCoins()
    {
        coins = PlayerPrefs.GetInt("Coins", 0);
        OnCoinsValueChanged?.Invoke(coins);
    }

    void SaveHighScore()
    {
        PlayerPrefs.SetInt("HighScore", highScore);
        PlayerPrefs.Save();
    }

    void LoadHighScore()
    {
        highScore = PlayerPrefs.GetInt("HighScore", 0);
        OnHighScoreChanged?.Invoke(highScore);
    }

    public GameState GetGameState()
    {
        return currentState;
    }

    public void SwitchState(GameState newState)
    {
        //Debug.Log("New state has been set to " + newState); 
        currentState = newState;
        OnGameStateChanged?.Invoke();
    }
    void SaveOwnedCats()
    {
        // Convert List<CatData> to List of cat names
        List<string> catNames = new List<string>();
        foreach (CatData cat in ownedCats)
        {
            catNames.Add(cat.catName);
        }

        // Serialize List of cat names to JSON
        string json = JsonUtility.ToJson(catNames);
        PlayerPrefs.SetString("OwnedCats", json);
        PlayerPrefs.Save();
    }
    void LoadOwnedCats()
    {
        Debug.Log("Loading owned cats...");
        Debug.Log("Number of catData loaded: " + catData.Count);

        // Iterate through catData to check if any cat is owned
        for (int i = 0; i < catData.Count; i++)
        {
            if (catData[i].isOwned)
            {
                Debug.Log("Owned cat found: " + catData[i].catName);
                AddOwnedCat(catData[i]);
            }
        }

        Debug.Log("Owned cats loaded: " + ownedCats.Count);
    }

    // Function to add a cat to the owned cats list
    public void AddOwnedCat(CatData cat)
    {
        if (!ownedCats.Contains(cat))
        {
            ownedCats.Add(cat);
            SaveOwnedCats(); // Save owned cats after adding
        }
    }

    // Function to remove a cat from the owned cats list
    public void RemoveOwnedCat(CatData cat)
    {
        if (ownedCats.Contains(cat))
        {
            ownedCats.Remove(cat);
            SaveOwnedCats(); // Save owned cats after removing
        }
    }

    // Function to check if a cat is owned
    public bool IsCatOwned(CatData cat)
    {
        return ownedCats.Contains(cat);
    }
} 

