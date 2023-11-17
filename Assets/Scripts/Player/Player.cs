using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] Transform min;
    [SerializeField] Transform max;
    
    private Vector2 startTouchPos, endTouchPos;
    private Vector3 startPlayerPos, endPlayerPos;
    private float moveTime;
    private float moveDuration = 0.1f;
    private bool isMoving = false;

 
    private void Update()
    {
        if (!isMoving && Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began) 
            startTouchPos = Input.GetTouch(0).position; 

        if (!isMoving && Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Ended)
        {
            endTouchPos = Input.GetTouch(0).position;

            if ((endTouchPos.x < startTouchPos.x) && transform.position.x > min.position.x) 
            {
                Debug.Log("Moving left");
                StartCoroutine(Move(0));
            }
            if ((endTouchPos.x > startTouchPos.x) && transform.position.x < max.position.x)
            {
                Debug.Log("Moving right");
                StartCoroutine(Move(1));
            }
        }
    }
    private IEnumerator Move(int direction)
    {
        isMoving = true; 

        switch (direction)
        {
            case 0:
                moveTime = 0f;
                startPlayerPos = transform.position;
                endPlayerPos = new Vector3(startPlayerPos.x - 1.75f, transform.position.y, transform.position.z);

                while (moveTime < moveDuration)
                {
                    moveTime += Time.deltaTime;
                    transform.position = Vector2.Lerp(startPlayerPos, endPlayerPos, moveTime / moveDuration);
                    yield return null;
                }
                break; 

            case 1:
                moveTime = 0f;
                startPlayerPos = transform.position;
                endPlayerPos = new Vector3(startPlayerPos.x + 1.75f, transform.position.y, transform.position.z);

                while (moveTime < moveDuration)
                {
                    moveTime += Time.deltaTime;
                    transform.position = Vector2.Lerp(startPlayerPos, endPlayerPos, moveTime / moveDuration);
                    yield return null;
                }
                break;
        }
        isMoving = false; 
    }
}
