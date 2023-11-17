using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Obstacles : MonoBehaviour
{
    public virtual void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log(other.name + "Collision Detected.");    
    }
}   
 