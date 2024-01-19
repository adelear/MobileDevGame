using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; 

public class PlayerAbilities : MonoBehaviour
{
    //Powerup Abilities
    Coroutine isInvincibile = null;
    Coroutine hasLasers = null;
    Coroutine hasScoreMultiplier = null;

    [SerializeField] private Image powerUpUI; 
    [SerializeField] private Sprite invincibiltyUI;
    [SerializeField] private Sprite scoreMultiplierUI;
    [SerializeField] private Sprite lasersUI;
    [SerializeField] private GameObject shield; 

    public bool invincibilityActive = false;
    public bool lasersActive = false;
    public bool scoreMultiplierActive = false;

    Coroutine powerUpCoroutine = null;
    private bool powerUpActive = false; 

    public void StartInvincibility()
    {
        Debug.Log("Invincibility started !");
        if (isInvincibile == null)
        {
            isInvincibile = StartCoroutine(Invincibility());
            return;
        }

        StopCoroutine(isInvincibile);
        isInvincibile = null;
        Invincibility();
    }

    IEnumerator Invincibility()
    {
        //CHANGES
        invincibilityActive = true;
        lasersActive = false;
        scoreMultiplierActive = false; 

        powerUpUI.gameObject.SetActive(true); 
        powerUpUI.sprite = invincibiltyUI;
        StartPowerUp(invincibiltyUI, 10.0f);
        shield.SetActive(true); 

        yield return new WaitForSeconds(10.0f);
        //RESET 
        invincibilityActive = false; 
        isInvincibile = null;
        powerUpUI.sprite = null;
        powerUpUI.gameObject.SetActive(false);
        shield.SetActive(false); 
    }

    public void StartLasers()
    {
        Debug.Log("Lasers started!");
        if (hasLasers == null)
        {
            hasLasers = StartCoroutine(Lasers());
            return;
        }

        StopCoroutine(hasLasers);
        hasLasers = null;
        Lasers();
    }

    IEnumerator Lasers()
    {
        //CHANGES
        lasersActive = true;
        invincibilityActive = false; 
        scoreMultiplierActive=false;
        shield.SetActive(false);
        powerUpUI.gameObject.SetActive(true);
        powerUpUI.sprite = lasersUI;
        StartPowerUp(lasersUI, 10.0f);

        yield return new WaitForSeconds(10.0f);

        //RESET 
        lasersActive = false;
        hasLasers = null;
        powerUpUI.sprite = null;
        powerUpUI.gameObject.SetActive(false);
    }

    public void StartScoreMultiplier()
    {
        Debug.Log("Score Multiplier started!");
        if (hasScoreMultiplier == null)
        {
            hasScoreMultiplier = StartCoroutine(ScoreMultiplier());
            return;
        }

        StopCoroutine(hasScoreMultiplier);
        hasScoreMultiplier = null;
        ScoreMultiplier();
    }

    IEnumerator ScoreMultiplier()
    {
        //CHANGES
        scoreMultiplierActive = true;
        lasersActive = false;
        invincibilityActive = false;
        shield.SetActive(false); 
        powerUpUI.gameObject.SetActive(true);
        powerUpUI.sprite = scoreMultiplierUI;
        StartPowerUp(scoreMultiplierUI, 10.0f); 

        yield return new WaitForSeconds(10.0f);

        //RESET 
        Debug.Log("Score Multiplier Ended!");
        scoreMultiplierActive = false;
        hasScoreMultiplier = null;
        powerUpUI.sprite = null;
        powerUpUI.gameObject.SetActive(false);
    }

    public void StartPowerUp(Sprite powerUpSprite, float duration)
    {
        Debug.Log($"{powerUpSprite.name} started!");
        if (powerUpCoroutine != null)
        {
            StopCoroutine(powerUpCoroutine);
            powerUpCoroutine = null;
        }

        powerUpCoroutine = StartCoroutine(PowerUpEffect(powerUpSprite, duration));
    }


    IEnumerator PowerUpEffect(Sprite powerUpSprite, float duration)
    {
        powerUpUI.gameObject.SetActive(true);
        powerUpUI.sprite = powerUpSprite;

        float startTime = Time.time;
        float flashStartTime = startTime + Mathf.Max(duration - 3.0f, 0f); // Start flashing 3 seconds before the end
        float flashInterval = 0.5f; // Flash every 0.5 seconds
        bool flashing = false;

        while (Time.time < (startTime + duration))
        {
            // Calculate remaining time until the power-up ends
            float remainingTime = startTime + duration - Time.time;

            // Toggle flashing every flashInterval seconds
            if (remainingTime <= 3.0f && Time.time % flashInterval < flashInterval / 2)
            {
                if (!flashing)
                {
                    powerUpUI.color = new Color(1f, 1f, 1f, 0.5f); // Set the UI to half transparent
                    flashing = true;
                }
            }
            else
            {
                if (flashing)
                {
                    powerUpUI.color = new Color(1f, 1f, 1f, 1f); // Set the UI to full transparent
                    flashing = false;
                }
            }

            yield return null;
        }

        //RESET 
        Debug.Log($"{powerUpSprite.name} Ended!");
        powerUpUI.sprite = null;
        powerUpUI.gameObject.SetActive(false);
        powerUpCoroutine = null;
    }
 
}
