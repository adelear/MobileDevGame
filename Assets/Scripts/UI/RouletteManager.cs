using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Rigidbody2D))]
public class RouletteManager : MonoBehaviour
{
    [SerializeField] GameObject wheelObject;
    public float rotatePower;
    private float stopPower;

    [SerializeField] private RectTransform rectTransform;
    [SerializeField]  private Rigidbody2D rb;
    int inRotate;
    float timeDur;
    public bool canRoll; 

    private void Start()
    {
        canRoll = true;
        stopPower = UnityEngine.Random.Range(200, 300); 
    }

    private void Update()
    {
        if (rb.angularVelocity > 0)
        {
            rb.angularVelocity -= stopPower * Time.deltaTime;
            rb.angularVelocity = Mathf.Clamp(rb.angularVelocity, 0, 1440);
        }

        if (rb.angularVelocity == 0 && inRotate == 1)
        {
            timeDur += 1 * Time.deltaTime;
            if (timeDur >= 0.5f)
            {
                GetReward();
                inRotate = 0;
                timeDur = 0;
            }
        }
    }

    public void Rotate()
    {
        if (inRotate == 0 && canRoll)
        {
            rb.AddTorque(rotatePower);
            inRotate = 1;
        }
    }

    public void GetReward()
    {
        float wheelRotation = rectTransform.rotation.eulerAngles.z; 

        if (wheelRotation > 0 && wheelRotation <= 45) Win(100);
        else if (wheelRotation > 45 && wheelRotation <= 90) Mystery();
        else if (wheelRotation > 90 && wheelRotation <= 135) Win(1);
        else if (wheelRotation > 135 && wheelRotation <= 180) TryAgain();
        else if (wheelRotation > 180 && wheelRotation <= 225) Win(10);
        else if (wheelRotation > 225 && wheelRotation <= 270) ContinuePlaying();
        else if (wheelRotation > 270 && wheelRotation <= 315) Win(50);
        else if (wheelRotation > 315 && wheelRotation <= 360) TryAgain();

    }

    public void Win(int coinNum)
    {
        Debug.Log("Yippeee");
        GameManager.Instance.CurrentCoins += coinNum;
        GameManager.Instance.DelayedGameOver(); 
    }

    public void Mystery()
    {
        int randomNum = UnityEngine.Random.Range(1, 6); 

        switch (randomNum)
        {
            case 1:
                Win(100); 
                break;
            case 2:
                Win(1); 
                break;
            case 3:
                ContinuePlaying(); 
                break;
            case 4:
                Win(50);
                break;
            case 5:
                ContinuePlaying(); 
                break;
            default:
                break; 
        }
    }

    public void TryAgain()
    {
        Debug.Log("TryAgain");
    }

    public void ContinuePlaying()
    {
        Debug.Log("ContinuePlaying");
        GameManager.Instance.SwitchState(GameManager.GameState.GAME);
        GameManager.Instance.Lives++;
        //ObstacleSpawner.Instance.SpawnObstacle(1);  
        canRoll = false; 
        wheelObject.SetActive(false);
    }
}
