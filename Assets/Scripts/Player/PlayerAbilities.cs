using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAbilities : MonoBehaviour
{
    //Powerup Abilities
    Coroutine isInvincibile = null;
    Coroutine hasLasers = null;
    Coroutine hasScoreMultiplier = null;

    public bool invincibilityActive = false;
    public bool lasersActive = false;
    public bool scoreMultiplierActive = false; 

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
        yield return new WaitForSeconds(15.0f);
        //RESET 
        invincibilityActive = false; 
        isInvincibile = null;
    }

    public void StartLasers()
    {
        Debug.Log("Lasers started!");
        if (hasLasers == null)
        {
            isInvincibile = StartCoroutine(Lasers());
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
        yield return new WaitForSeconds(15.0f);
        //RESET 
        lasersActive = false; 
        isInvincibile = null;
    }

    public void StartScoreMultiplier()
    {
        Debug.Log("Score Multiplier started!");
        if (hasScoreMultiplier == null)
        {
            isInvincibile = StartCoroutine(ScoreMultiplier());
            return;
        }

        StopCoroutine(hasScoreMultiplier);
        hasScoreMultiplier = null;
        Lasers();
    }

    IEnumerator ScoreMultiplier()
    {
        //CHANGES
        scoreMultiplierActive = true; 
        yield return new WaitForSeconds(15.0f);
        //RESET 
        Debug.Log("Score Multiplier Ended!");
        scoreMultiplierActive = false;
        isInvincibile = null;
    }
}
  