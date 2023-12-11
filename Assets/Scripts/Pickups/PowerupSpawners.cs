using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerupSpawners : MonoBehaviour
{
    public GameObject[] pickups;
    public Transform[] spawnPoints;

    private void Start()
    {
        int pickupSize = pickups.Length;

        for (int i = 0; i < spawnPoints.Length; i++)
        {
            int randomPickup = Random.Range(0, pickupSize);

            Instantiate(pickups[randomPickup], spawnPoints[i].position, spawnPoints[i].rotation);
            Destroy(gameObject); 

        }
    } 
}
