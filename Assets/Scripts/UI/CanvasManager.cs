using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;
using UnityEngine.Audio;
using TMPro;

public class CanvasManager : MonoBehaviour
{
    [SerializeField] AudioManager asm;  
    [SerializeField] AudioSource audioSource;
    [SerializeField] AudioClip pauseSound;
    [SerializeField] AudioClip gameMusic;
    [SerializeField] AudioClip titleMusic;
    [SerializeField] AudioClip buttonSound; 

    [SerializeField] AudioMixer audioMixer;

    [Header("Buttons")]
    [SerializeField] Button startButton;
    [SerializeField] Button settingsButton;
    [SerializeField] Button backButton;
    [SerializeField] Button quitButton;
    [SerializeField] Button pauseButton; 
    [SerializeField] Button returnToMenuButton;
    [SerializeField] Button resumeGame;

    [Header("Menus")]
    [SerializeField] GameObject mainMenu;
    [SerializeField] GameObject pauseMenu;
    [SerializeField] GameObject settingsMenu;

    [Header("Text")]
    [SerializeField] TMP_Text scoreText;
    [SerializeField] TMP_Text highScoreText; 

    [Header("Sliders")]
    [SerializeField] Slider masterSlider;
    [SerializeField] Slider musicSlider;
    [SerializeField] Slider sfxSlider;

    [SerializeField] PlayerAbilities player; 


    private float timeElapsed = 0f;
    private int lastSecond = 0;

    void Start()
    {
        GameObject playerObject = GameObject.Find("Player");
        if (playerObject != null )
        {
            player = playerObject.GetComponent<PlayerAbilities>(); 
        }

        asm = GetComponent<AudioManager>();
        if (startButton)
        {
            startButton.onClick.AddListener(StartGame);
            EventTrigger startButtonTrigger = startButton.gameObject.AddComponent<EventTrigger>();
            AddPointerEnterEvent(startButtonTrigger, PlayButtonSound); 
        }

        if (settingsButton)
        {
            EventTrigger settingsButtonTrigger = settingsButton.gameObject.AddComponent<EventTrigger>();
            AddPointerEnterEvent(settingsButtonTrigger, PlayButtonSound);
            settingsButton.onClick.AddListener(ShowSettingsMenu);
        }


        if (backButton)
        {
            backButton.onClick.AddListener(ShowMainMenu);
            EventTrigger backButtonTrigger = backButton.gameObject.AddComponent<EventTrigger>();
            AddPointerEnterEvent(backButtonTrigger, PlayButtonSound);
        }

        if (quitButton)
        {
            quitButton.onClick.AddListener(Quit);
            EventTrigger quitButtonTrigger = quitButton.gameObject.AddComponent<EventTrigger>();
            AddPointerEnterEvent(quitButtonTrigger, PlayButtonSound); 
        }

        if (masterSlider)
        {
            masterSlider.onValueChanged.AddListener((value) => OnSliderValueChanged(value, "MasterVol"));
        }

        if (musicSlider)
        {
            musicSlider.onValueChanged.AddListener((value) => OnSliderValueChanged(value, "MusicVol"));
        }

        if (sfxSlider)
        {
            sfxSlider.onValueChanged.AddListener((value) => OnSliderValueChanged(value, "SFXVol")); 
        }

        if (scoreText) 
        {
            GameManager.Instance.OnScoreValueChanged.AddListener((value) => UpdateScoreText(value));
            scoreText.text = "00000" + GameManager.Instance.Score.ToString();
        }

        if (highScoreText)
        {
            GameManager.Instance.OnHighScoreChanged.AddListener((value) => UpdateHighScoreText(value));
            highScoreText.text = GameManager.Instance.HighScore.ToString("D6");
        }


        if (resumeGame)
        {
            EventTrigger resumeGameTrigger = resumeGame.gameObject.AddComponent<EventTrigger>();
            AddPointerEnterEvent(resumeGameTrigger, PlayButtonSound); 
            resumeGame.onClick.AddListener(UnpauseGame);
        }

        if (returnToMenuButton)
        {
            EventTrigger returnToMenuTrigger = returnToMenuButton.gameObject.AddComponent<EventTrigger>();
            AddPointerEnterEvent(returnToMenuTrigger, PlayButtonSound);
            returnToMenuButton.onClick.AddListener(LoadTitle);
        }

        if (pauseButton)
        {
            pauseButton.onClick.AddListener(PauseMenu); 

        }
    }

    void LoadTitle()
    {
        SceneTransitionManager.Instance.LoadScene("MainMenu"); 
    }
    void UnpauseGame()
    {
        Time.timeScale = 1.0f;
        GameManager.Instance.SwitchState(GameManager.GameState.GAME);
        pauseMenu.SetActive(false);
    }

    void PauseMenu()
    {
        pauseMenu.SetActive(!pauseMenu.activeSelf);
        asm.PlayOneShot(pauseSound, false); 
    }

    void UpdateScoreText(int value)
    {
        scoreText.text = value.ToString("D6"); 
    }

    void UpdateHighScoreText(int value)
    {
        highScoreText.text =  value.ToString("D6");
        if (GameManager.Instance.Score >= GameManager.Instance.HighScore) highScoreText.gameObject.SetActive(false); 
    }
 

    void Update()
    {
        timeElapsed += Time.deltaTime;
        // Check if a whole second has passed
        int milliseconds = Mathf.FloorToInt(timeElapsed * 10);
        if (milliseconds > lastSecond)
        {
            if (GameManager.Instance.GetGameState()==GameManager.GameState.GAME && player!=null)
            {
                if (player.scoreMultiplierActive)
                {
                    GameManager.Instance.Score += 2;
                    lastSecond = milliseconds;
                }
                else
                {
                    GameManager.Instance.Score += 1;
                    lastSecond = milliseconds;
                }
            }
        }

        if (!pauseMenu) return;

        if (pauseMenu.activeSelf)
        {
            Time.timeScale = 0f;
            GameManager.Instance.SwitchState(GameManager.GameState.PAUSE);
            pauseMenu.SetActive(true);
        }

        else
        {
            UnpauseGame();
        }
    }
    void ShowSettingsMenu()
    {
        mainMenu.SetActive(false);
        settingsMenu.SetActive(true);
        if (masterSlider)
        {
            float value;
            audioMixer.GetFloat("MasterVol", out value);
            masterSlider.value = value + 80;
        }
        else
        {
            Debug.LogError("Audio Mixer is not assigned.");
        }

        if (musicSlider)
        {
            float value;
            audioMixer.GetFloat("MusicVol", out value);
            musicSlider.value = value + 80; 
        }

        if (sfxSlider)
        {
            float value;
            audioMixer.GetFloat("SFXVol", out value);
            sfxSlider.value = value + 80;
        }
    }

    void ShowMainMenu()
    {
        settingsMenu.SetActive(false);
        mainMenu.SetActive(true);
    }

    void StartGame()
    {
        SceneTransitionManager.Instance.LoadScene("Level"); ;
        Time.timeScale = 1.0f;
        if (audioSource != null)
        {
            audioSource.Stop();
            audioSource.clip = gameMusic;
            audioSource.Play();
        }
        else
        {
            Debug.LogError("Audio Source is not assigned.");
        }
    }

    void OnSliderValueChanged(float value, string volume)
    {
        audioMixer.SetFloat(volume, value - 80);
    }

    private void AddPointerEnterEvent(EventTrigger trigger, UnityEngine.Events.UnityAction action)
    {
        EventTrigger.Entry entry = new EventTrigger.Entry();
        entry.eventID = EventTriggerType.PointerEnter;
        entry.callback.AddListener((eventData) => action.Invoke());
        trigger.triggers.Add(entry);
    } 

    void PlayButtonSound()
    {
        asm.PlayOneShot(buttonSound, false);
    }

    void Quit()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit(); 
#endif
    }
}