using UnityEngine;

public class DestroyIfNoChildren : MonoBehaviour
{
    void Update()
    {
        if (transform.childCount == 0)
        {
            Destroy(gameObject);
        }
    }
} 
