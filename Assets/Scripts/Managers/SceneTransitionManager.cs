using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTransitionManager : MonoBehaviour
{ 
    public static SceneTransitionManager Instance { get; private set; }

    [Header("Components")]
    [SerializeField] private CanvasGroup fadeImg;
    [SerializeField] private float fadeDuration = 0.3f;

    private bool isTransitioning = false;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;

        if (Instance != this)
            Destroy(this.gameObject);

        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        StartCoroutine(FadeOut());
    }

    private void OnLevelWasLoaded(int level)
    {
        StartCoroutine(FadeOut());
    }

    public void LoadScene(string sceneName)
    {
        if (isTransitioning) return;

        isTransitioning = true;
        StartCoroutine(FadeIn(sceneName));
    }

    public void RestartScene()
    {
        string scene = SceneManager.GetActiveScene().name;
        LoadScene(scene);
    }

    private IEnumerator FadeIn(string sceneName)
    {
        float timeElapsed = 0.0f;
        while (timeElapsed <= fadeDuration)
        {
            timeElapsed += Time.unscaledDeltaTime;
            fadeImg.alpha = Mathf.Lerp(0.0f, 1.0f, timeElapsed / fadeDuration);
            yield return null;
        }

        fadeImg.alpha = 1.0f;
        yield return null;

        SceneManager.LoadScene(sceneName);
    }

    private IEnumerator FadeOut()
    {
        isTransitioning = true;

        float timeElapsed = 0.0f;

        fadeImg.alpha = 1.0f;

        while (timeElapsed <= fadeDuration)
        {

            fadeImg.alpha = Mathf.Lerp(1.0f, 0.0f, timeElapsed / fadeDuration);
            timeElapsed += Time.unscaledDeltaTime;
            yield return null;
        }

        fadeImg.alpha = 0.0f;

        isTransitioning = false;
    }


}