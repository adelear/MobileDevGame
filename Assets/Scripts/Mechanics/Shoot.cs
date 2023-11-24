using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shoot : MonoBehaviour
{
    [SerializeField] private Transform spawnPoint;
    [SerializeField] private Lasers laserPrefab;
    public event Action<GameObject> OnLaserSpawned;

    public void ShootLaser()
    {
        Lasers curLaser = Instantiate(laserPrefab, spawnPoint.position, spawnPoint.rotation);
        OnLaserSpawned?.Invoke(gameObject); 
    }
}
