using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Powerups : MovingObjects
{
    public enum PickupType
    {
        Invincibility, 
        ScoreMultiplier,
        Score,  
        Laser,
        Nothing
    }
    public PickupType currentPickup;
    public AudioClip pickupSound;

    public override void OnTriggerEnter2D(Collider2D collision)  
    {  
        if (collision.gameObject.CompareTag("Player"))
        {
            //AudioManager.PlayOneShot(pickupSound, false);   

            if (currentPickup == PickupType.Invincibility)
            {
                PlayerAbilities abilities = collision.gameObject.GetComponent<PlayerAbilities>();
                abilities.StartInvincibility();
                Destroy(gameObject);
                return;
            }
            if (currentPickup == PickupType.ScoreMultiplier)
            {
                PlayerAbilities abilities = collision.gameObject.GetComponent<PlayerAbilities>();
                abilities.StartScoreMultiplier();
                Destroy(gameObject); 
                return;
            }
            if (currentPickup == PickupType.Score)
            {
                GameManager.Instance.Score+=10;
                Destroy(gameObject);
                return;
            }
            if (currentPickup == PickupType.Laser)
            {
                PlayerAbilities abilities = collision.gameObject.GetComponent<PlayerAbilities>(); 
                abilities.StartLasers(); 
                Destroy(gameObject);
                return; 
            }

            else
            {
                Destroy(gameObject);
                return; 
            }
            
        }
    }
}
