using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] Transform min;
    [SerializeField] Transform max;

    //Swipe Mechanics
    private Vector2 startTouchPos, endTouchPos;
    private Vector3 startPlayerPos, endPlayerPos;

    //Movement 
    private float moveTime;
    private float moveDuration = 0.1f;
    private bool isMoving = false;

    PlayerAbilities ability;
    Shoot shoot;
    private bool canShoot = true;
    private float cooldownDuration = 0.2f; 

    private void Start()
    {
        ability = GetComponent<PlayerAbilities>(); 
        shoot = GetComponent<Shoot>();

    }

    private void Update()
    {
        if (!isMoving && Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
        {
            startTouchPos = Input.GetTouch(0).position;
            if (ability.lasersActive && canShoot)
            {
                shoot.ShootLaser();
                StartCoroutine(Cooldown()); 
            }
        }

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

    private IEnumerator Cooldown()
    {
        canShoot = false;
        yield return new WaitForSeconds(cooldownDuration);
        canShoot = true; 
    }
}
